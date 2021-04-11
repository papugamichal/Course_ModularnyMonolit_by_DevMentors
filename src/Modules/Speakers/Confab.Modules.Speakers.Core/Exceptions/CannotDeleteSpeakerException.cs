using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Exceptions;


namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class CannotDeleteSpeakerException : ConfabException
    {
        public Guid Id { get; }

        public CannotDeleteSpeakerException(Guid id) : base($"Conference with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
