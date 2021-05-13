using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;
}