using System;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Shared.Infrastructure.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResposne Map(Exception exception);
    }
}
