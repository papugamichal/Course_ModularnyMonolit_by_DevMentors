using Confab.Shared.Abstraction.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Queries
{
    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                var handler = scope.ServiceProvider.GetRequiredService(handlerType);

                return await (Task<TResult>) handlerType
                    .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))
                    ?.Invoke(handler, new[] { query });
            }
        }
    }
}
