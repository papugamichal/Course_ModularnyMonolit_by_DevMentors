using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Exceptions
{
    public class MissingSubmissionSpeakersException : ConfabException
    {
        public Guid SubmissionId { get; }

        public MissingSubmissionSpeakersException(Guid submissionId) 
            : base($"Submission with Id: '{submissionId}' has missing speakers.")
        {
            SubmissionId = submissionId;
        }
    }
}
