using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events
{
    public sealed class SubmissionCreated : IEvent
    {
        public SubmissionCreated(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
