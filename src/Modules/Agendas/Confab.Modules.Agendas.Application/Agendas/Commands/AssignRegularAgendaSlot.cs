using System;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public sealed record AssignRegularAgendaSlot(Guid AgendaTrackId, Guid AgendaSlotId, Guid AgendaItemId) : ICommand;
}