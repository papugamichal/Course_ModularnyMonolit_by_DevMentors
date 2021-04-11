using System;
using System.Runtime.CompilerServices;
using Confab.Modules.Speakers.Core.DAL;
using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.Policies;
using Confab.Modules.Speakers.Core.Repository;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Modules.Speakers.API")]
namespace Confab.Modules.Speakers.Core
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCore(this IServiceCollection services)
        {
            services
                .AddPostgres<SpeakersDbContext>()
                .AddSingleton<ISpeakerDeletionPolicy, SpeakerDeletionPolicy>()
                .AddScoped<ISpeakerRepository, SpeakersRepository>()
                .AddScoped<ISpeakerService, SpeakerService>()
                ;

            return services;
        }
    }
}
