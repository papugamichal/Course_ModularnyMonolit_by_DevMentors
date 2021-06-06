using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record UserSignedIn(Guid UserId, string Email) : IEvent;
}
