using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Exceptions
{
    internal class SubmissionNotFoundException : ConfabException
    {
        public SubmissionNotFoundException(Guid submissionId) 
            : base($"Submission with Id: '{submissionId}' was not found.")
        {
            SubmissionId = submissionId;
        }

        public Guid SubmissionId { get; }
    }
}
