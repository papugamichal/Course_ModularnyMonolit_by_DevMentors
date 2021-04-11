using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstraction.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Conferences.API
{
    internal class ConferencesModule : IModule
    {
        public const string BasePath = "conferences-module";
        public const string ConfenerencesPolicy = "conferences";
        public const string HostsPolicy = "hosts";
        public string Name { get; } = "Conferences";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            ConfenerencesPolicy, HostsPolicy
        };

        public void Register(IServiceCollection services)
        {
            services
                .AddCore()
                ;
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
