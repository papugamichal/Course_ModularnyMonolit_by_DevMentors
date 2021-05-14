using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Attendances.Application.Events.External
{
    public record TicketPurchased(Guid TicketId, Guid ConferenceId, Guid UserId) : IEvent
    {
    }
}
