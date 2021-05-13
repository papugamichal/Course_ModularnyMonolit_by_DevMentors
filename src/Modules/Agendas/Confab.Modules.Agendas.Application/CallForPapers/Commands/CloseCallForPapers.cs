using System;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands
{

    public record CloseCallForPapers(Guid ConferenceId) : ICommand;
}
