using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public record PlaceholderAssignedToAgendaSlot(Guid Id, string Placeholder) : IEvent;
}