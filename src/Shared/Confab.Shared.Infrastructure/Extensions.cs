using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstraction;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedInfrastructure(
            this IServiceCollection services)
        {
            services
                .AddSingleton<IClock, UtcClock>()
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                })
                ;

            return services;
        }

        public static IApplicationBuilder UseSharedInfrastructure(
            this IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Confab API!"));
            });

            return app;
        }
    }
}
