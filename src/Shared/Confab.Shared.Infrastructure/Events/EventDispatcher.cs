using Confab.Shared.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(@event);
                }
            }
        }
    }
}
