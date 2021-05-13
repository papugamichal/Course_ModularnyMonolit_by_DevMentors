using System;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public record ChangeAgendaTrackName(Guid Id, string Name) : ICommand;
}