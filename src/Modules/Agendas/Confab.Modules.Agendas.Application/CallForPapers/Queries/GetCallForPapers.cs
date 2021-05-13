using System;
using Confab.Modules.Agendas.Application.CallForPapers.DTO;
using Confab.Shared.Abstraction.Queries;

namespace Confab.Modules.Agendas.Application.CallForPapers.Queries
{
    public class GetCallForPapers : IQuery<CallForPapersDto>
    {
        public Guid ConferenceId { get; set; }
    }
}
