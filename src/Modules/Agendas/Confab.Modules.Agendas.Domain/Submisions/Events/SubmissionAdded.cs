using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Shared.Abstraction.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Events
{
    public record SubmissionAdded(Submission Submission) : IDomainEvent;
}
