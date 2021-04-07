using System.Net;

namespace Confab.Shared.Abstraction.Exceptions
{
    public record ExceptionResposne(object Respone, HttpStatusCode StatusCode);
}
