using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record UserSignedUp(Guid UserId, string Email) : IEvent;
}
