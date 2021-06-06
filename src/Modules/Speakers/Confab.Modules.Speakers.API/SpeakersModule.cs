using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Confab.Modules.Speakers.Core;
using Confab.Shared.Abstraction.Modules;
using System.Collections.Generic;
using Confab.Shared.Infrastructure.Modules;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.API
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";
        public const string Policy = "speakers";

        public string Name { get; } = "Speakers";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            Policy
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequest()
                .Subscribe<SpeakerDto, object>("speakers/create", async (request, provider) =>
                {
                    var service = provider.GetRequiredService<ISpeakerService>();
                    await service.AddAsync(request);
                    return Task.CompletedTask;
                });
        }
    }
}
