using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerAlreadyExistsException : ConfabException
    {
        public Guid Id { get; }

        public SpeakerAlreadyExistsException(Guid id) : base($"Speaker with ID: '{id}' already exists.")
        {
            Id = id;
        }
    }
}
