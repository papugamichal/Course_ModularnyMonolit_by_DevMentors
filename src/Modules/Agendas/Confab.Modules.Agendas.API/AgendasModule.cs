using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstraction.Modules;
using Confab.Shared.Abstraction.Queries;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.API
{
    internal class AgendasModule : IModule
    {
        public const string BasePath = "agendas-module";
        public string Name { get; } = "Agendas";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new string[] { };

        public void Register(IServiceCollection services)
        {
            services
                .AddDomain()
                .AddApplication()
                .AddInfrastructure()
                ;
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseModuleRequest()
                .Subscribe<GetRegularAgendaSlot, RegularAgendaSlotDto>("agendas/slots/regular/get",
                (query, sp) => sp.GetRequiredService<IQueryDispatcher>().QueryAsync(query));
        }
    }
}
