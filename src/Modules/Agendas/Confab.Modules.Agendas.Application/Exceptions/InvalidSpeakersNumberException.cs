using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Exceptions
{
    internal class InvalidSpeakersNumberException : ConfabException
    {
        public InvalidSpeakersNumberException(Guid submissionId) 
            : base($"Submission with Id: '{submissionId}' has invalid speakers.")
        {
            SubmissionId = submissionId;
        }

        public Guid SubmissionId { get; }
    }
}
