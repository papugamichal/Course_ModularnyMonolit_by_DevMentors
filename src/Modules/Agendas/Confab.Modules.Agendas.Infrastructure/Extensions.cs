using System.Runtime.CompilerServices;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Domain.CallForPapers.Repositories;
using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Modules.Agendas.Infrastructure.EF.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Modules.Agendas.API")]
namespace Confab.Modules.Agendas.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services
                .AddPostgres<AgendasDbContext>()
                .AddScoped<ISpeakerRepository, SpeakerRepository>()
                .AddScoped<ISubmissionRepository, SubmissionRepository>()
                .AddScoped<IAgendaItemsRepository, AgendaItemsRepository>()
                .AddScoped<ICallForPapersRepository, CallForPapersRepository>()
                .AddScoped<IAgendaTracksRepository, AgendaTracksRepository>()
            ;
            return services;
        }
    }
}
