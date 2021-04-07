using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.API")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddSingleton<IHostRepository, InMemoryHostRepository>()
                .AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>()
                .AddScoped<IHostService, HostService>()

                .AddSingleton<IConferenceRepository, InMemoryConferenceRepository>()
                .AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>()
                .AddScoped<IConferenceService, ConferenceService>()
                ;
            return services;
        }
    }
}
