using Confab.Modules.Tickets.Core;
using Confab.Shared.Abstraction.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Confab.Modules.Tickets.API
{
    internal class TicketsModule : IModule
    {
        public const string BasePath = "tickets-module";
        public const string TicketsPolicy = "TicketsPolicy";

        public string Name { get; } = "Tickets";

        public string Path { get; } = BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            "tickets"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
