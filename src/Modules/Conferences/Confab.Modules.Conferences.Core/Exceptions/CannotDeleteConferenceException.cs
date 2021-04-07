using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    public class CannotDeleteConferenceException : ConfabException
    {
        public Guid Id { get; }

        public CannotDeleteConferenceException(Guid id) : base($"Conference with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
