using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Events
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
