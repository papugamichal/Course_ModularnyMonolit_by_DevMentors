using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Confab.Modules.Speakers.Core;
using Confab.Shared.Abstraction.Modules;

namespace Confab.Modules.Speakers.API
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";

        public string Name { get; } = "Speakers";

        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
