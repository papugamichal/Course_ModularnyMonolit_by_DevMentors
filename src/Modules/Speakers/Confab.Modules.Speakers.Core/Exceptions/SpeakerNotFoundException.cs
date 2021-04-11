using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerNotFoundException : ConfabException
    {
        public Guid Id { get; }

        public SpeakerNotFoundException(Guid id) : base($"Speaker with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}
