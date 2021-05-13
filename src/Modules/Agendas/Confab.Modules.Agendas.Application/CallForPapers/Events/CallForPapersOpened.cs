using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.CallForPapers.Events
{
    internal record CallForPapersOpened(Guid ConferenceId, DateTime From, DateTime To) : IEvent;
}
