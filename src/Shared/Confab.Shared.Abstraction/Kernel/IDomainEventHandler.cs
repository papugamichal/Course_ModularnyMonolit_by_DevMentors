using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Kernel
{
    public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
