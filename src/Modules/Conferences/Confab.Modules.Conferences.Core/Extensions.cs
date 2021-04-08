using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Confab.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.API")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddPostgres<ConferencesDbContext>()
                .AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>()
                //.AddSingleton<IHostRepository, InMemoryHostRepository>()
                .AddScoped<IHostRepository, HostRepository>()
                .AddScoped<IHostService, HostService>()

                .AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>()
                //.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>()
                .AddScoped<IConferenceRepository, ConferencesRepository>()
                .AddScoped<IConferenceService, ConferenceService>()
                ;
            return services;
        }
    }
}
