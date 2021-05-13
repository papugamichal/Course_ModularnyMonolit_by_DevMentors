using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersCreated(Guid ConferenceId) : IEvent;
}
