using System;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events
{
    public sealed class SubmissionApproved : IEvent
    {
        public SubmissionApproved(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
