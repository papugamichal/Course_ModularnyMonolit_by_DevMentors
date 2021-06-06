using System;
using System.Collections.Generic;
using Confab.Shared.Abstraction.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga
{
    public class SagaModule : IModule
    {
        public const string BasePath = "saga-module";
        public string Name { get; } = "Saga";
        public string Path => BasePath;

        public IEnumerable<string> Policies => Array.Empty<string>();

        public void Register(IServiceCollection services)
        {
            services.AddSaga();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
