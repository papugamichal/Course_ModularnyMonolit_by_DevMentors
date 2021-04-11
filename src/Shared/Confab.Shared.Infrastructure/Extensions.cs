using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstraction.Modules;
using Confab.Shared.Abstraction.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
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
                .AddAuth(modules)
                .AddHostedService<AppInitializer>()
                .AddPostgres()
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
            app.UseErrorHanling();
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
    }
}
