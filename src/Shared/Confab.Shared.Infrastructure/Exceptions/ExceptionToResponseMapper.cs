using System;
using System.Collections.Concurrent;
using System.Net;
using Confab.Shared.Abstraction.Exceptions;
using Humanizer;

namespace Confab.Shared.Infrastructure.Exceptions
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResposne Map(Exception exception)
            => exception switch
            {
                ConfabException ex => new ExceptionResposne(
                    new ErrorResponse(new Error(GetErroCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                _ => new ExceptionResposne(new ErrorResponse(
                    new Error("error", "There was generic error")), HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);
        
        private record ErrorResponse(params Error[] Error);

        private static string GetErroCode(object exception)
        {
            var type = exception.GetType();
            var code = type.Name.Underscore().Replace("_exception", string.Empty);
            return Codes.GetOrAdd(type, code);
        } 
    }
}
