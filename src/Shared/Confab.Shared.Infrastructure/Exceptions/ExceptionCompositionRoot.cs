using System;
using System.Linq;
using Confab.Shared.Abstraction.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal interface IExceptionCompositionRoot
    {
        ExceptionResposne Map(Exception exception);
    }

    internal class ExceptionCompositionRoot : IExceptionCompositionRoot
    {
        private readonly IServiceProvider serviceProvider;

        public ExceptionCompositionRoot(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ExceptionResposne Map(Exception exception)
        {
            using var scope = this.serviceProvider.CreateScope();
            var mappers = scope.ServiceProvider.GetServices<IExceptionToResponseMapper>().ToArray();
            var nonDefaultMappers = mappers.Where(x => x is not ExceptionToResponseMapper).ToArray();
            var result = nonDefaultMappers
                .Select(x => x.Map(exception))
                .SingleOrDefault(x => x is not null);

            if (result is not null)
            {
                return result;
            }

            var defaultMapper = mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);

            return defaultMapper?.Map(exception);
        }
    }
}
