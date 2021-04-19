using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Modules.Agendas.Infrastructure.EF.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                ;
            return services;
        }
    }
}
