using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Exceptions
{
    public class InvalidSubmissionStatusException : ConfabException
    {
        public Guid SubmissionId { get; }

        public InvalidSubmissionStatusException(Guid submissionId, string desiredStatus, string currentStatus) 
            : base($"Cannot change status of submission with Id: '{submissionId}' from {currentStatus} to {desiredStatus} ")
        {
            SubmissionId = submissionId;
        }
    }
}
