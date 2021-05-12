using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Application.Submissions.Events;
using Confab.Modules.Agendas.Domain.Submisions.Consts;
using Confab.Modules.Agendas.Domain.Submisions.Events;
using Confab.Shared.Abstraction.Kernel;
using Confab.Shared.Abstraction.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Services
{
    public class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                SubmissionAdded e => new SubmissionCreated(e.Submission.Id),
                SubmissionStatusChanged { Status: SubmissionStatus.Approved } e => new SubmissionApproved(e.Submission.Id),
                SubmissionStatusChanged { Status: SubmissionStatus.Rejected } e => new SubmissionRejected(e.Submission.Id),
                _ => null
            };

        public IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events)
        {
            if (events is null || !events.Any()) return Enumerable.Empty<IMessage>();

            return events.Select(@event => Map(@event));
        }
    }
}
