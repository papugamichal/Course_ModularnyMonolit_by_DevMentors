using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Saga.Messages
{
    internal record SpeakerCreated(Guid Id, string FullName) : IEvent;
}
