using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Kernel
{
    internal class DomainEventHandlerDispatcher : IDomainEventHandlerDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public DomainEventHandlerDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(params IDomainEvent[] events)
        {
            if (events is null || events.Any()) return;


            using(var scope = this.serviceProvider.CreateScope())
            {
                foreach(var @event in events)
                {
                    var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                    var handlers = scope.ServiceProvider.GetServices(handlerType);

                    var tasks = handlers.Select(x =>
                        (Task)handlerType
                            .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                            ?.Invoke(x, new[] { @event }));

                    await Task.WhenAll(tasks);
                }
            }
        }
    }
}
