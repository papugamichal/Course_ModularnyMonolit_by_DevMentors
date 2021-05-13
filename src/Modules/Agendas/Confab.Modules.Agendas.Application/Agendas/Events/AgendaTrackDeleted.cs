using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public record AgendaTrackDeleted(Guid Id) : IEvent;
}