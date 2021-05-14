using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Attendances.Application.Events.External
{
    public record AgendaItemAssignedToAgendaSlot(Guid Id, Guid AgendaItemId) : IEvent;
}