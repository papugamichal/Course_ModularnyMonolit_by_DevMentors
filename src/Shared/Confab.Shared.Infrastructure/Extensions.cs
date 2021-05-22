using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstraction.Contexts;
using Confab.Shared.Abstraction.Modules;
using Confab.Shared.Abstraction.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Contexts;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Kernel;
using Confab.Shared.Infrastructure.Messaging;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Queries;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        public const string CorsPolicy = "CorsPolicy";

        public static IServiceCollection AddSharedInfrastructure(
            this IServiceCollection services,
            IList<Assembly> assemblies, 
            IList<IModule> modules 
            )
        {
            var disablesModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    if (!key.Contains("module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        disablesModules.Add(key.Split(":")[0]);
                    }
                }
            }

            services
                .AddCors(cors =>
                {
                    cors.AddPolicy(CorsPolicy, x =>
                        x.WithOrigins("*")
                            .WithMethods("POST", "GET", "PUT")
                            .WithHeaders("Content-Type", "Authorization")
                    );
                })
                .AddSwaggerGen(swagger =>
                {
                    swagger.CustomSchemaIds(x => x.FullName); // use FullName for DTO generation
                    swagger.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Title = "Confab API",
                        Version = "v1"
                    });
                })
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IContextFactory, ContextFactory>()
                .AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create())
                .AddModuleInfo(modules)
                .AddModuleRequests(assemblies)
                .AddAuth(modules)
                .AddEvents(assemblies)
                .AddCommands(assemblies)
                .AddQueries(assemblies)
                .AddDominEvents(assemblies)
                .AddMessaging()
                .AddHostedService<AppInitializer>()
                .AddPostgres()
                .AddTransactionalDecorators()
                .AddSingleton<IClock, UtcClock>()
                .AddErrorHandling()
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disablesModules)
                    {
                        var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                    {
                        manager.ApplicationParts.Remove(part);
                    }

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                })
                ;

            return services;
        }

        public static IApplicationBuilder UseSharedInfrastructure(
            this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            app.UseErrorHanling();
            app.UseSwagger();
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Confab API";
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        public static string GetModuleName(this object value)
        {
            return value?.GetType().GetModuleName() ?? string.Empty;
        }

        public static string GetModuleName(this Type type)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.StartsWith("Confab.Modules.") 
                ? type.Namespace.Split('.')[2].ToLowerInvariant()
                : string.Empty;
        }
    }
}
