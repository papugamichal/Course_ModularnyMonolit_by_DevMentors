using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class AlreadyParticipatingInEventException : ConfabException
    {
        public AlreadyParticipatingInEventException() : base("Already participating in the selected agenda item.")
        {
        }
    }
}