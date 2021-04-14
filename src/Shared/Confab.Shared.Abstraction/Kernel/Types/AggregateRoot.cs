using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Kernel.Types
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; protected set; }
        public int Version { get; set; }
        public IEnumerable<IDomainEvent> Event => domainEvents;


        private readonly List<IDomainEvent> domainEvents = new();
        private bool versionIncremented;

        protected void AddEvent(IDomainEvent @event)
        {
            if (!domainEvents.Any() && !versionIncremented)
            {
                versionIncremented = true;
                Version++;
            }

            domainEvents.Add(@event);
        }

        public void ClearEvents() => domainEvents.Clear();

        protected void IncrementVersion()
        {
            if (!versionIncremented)
            {
                versionIncremented = true;
                Version++;
            }
        }
    }

    public abstract class AggregateRoot : AggregateRoot<AggregateId>
    {

    }
}
