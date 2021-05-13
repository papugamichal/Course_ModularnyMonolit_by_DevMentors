using System;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public record AssignPlaceholderAgendaSlot(Guid AgendaSlotId, Guid AgendaTrackId, string Placeholder) : ICommand;
}