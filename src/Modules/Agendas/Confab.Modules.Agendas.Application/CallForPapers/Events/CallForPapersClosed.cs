using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersClosed(Guid ConferenceId) : IEvent;
}
