using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confab.Shared.Abstraction.Modules;
using Confab.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Bootstrapper
{
    public class Startup
    {
        private IList<IModule> modules;
        private IList<Assembly> assemlies;

        public Startup(IConfiguration configuration)
        {
            this.assemlies = ModuleLoader.LoadAssemblies(configuration);
            this.modules = ModuleLoader.LoadModules(assemlies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSharedInfrastructure(assemlies, modules)
                ;

            foreach (var module in this.modules)
            {
                module.Register(services);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app
                .UseSharedInfrastructure()
                ;

            foreach (var module in this.modules)
            {
                module.Use(app);
            }

            logger.LogInformation($"Modules loaded:  {string.Join(", ", (modules.Select(x => x.Name)))}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Confab API!"));
            });

            this.assemlies.Clear();
            this.modules.Clear();
        }
    }
}
