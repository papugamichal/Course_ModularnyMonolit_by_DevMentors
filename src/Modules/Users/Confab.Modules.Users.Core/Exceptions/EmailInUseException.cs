using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : ConfabException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}