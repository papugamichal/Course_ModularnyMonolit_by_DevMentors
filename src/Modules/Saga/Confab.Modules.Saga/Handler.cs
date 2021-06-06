using System;
using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Saga
{
    internal class Handler : 
        IEventHandler<SpeakerCreated>, 
        IEventHandler<UserSignedIn>, 
        IEventHandler<UserSignedUp>
    {
        private readonly ISagaCoordinator sagaCoordinator;

        public Handler(ISagaCoordinator sagaCoordinator)
        {
            this.sagaCoordinator = sagaCoordinator;
        }

        public Task HandleAsync(SpeakerCreated @event)
            => sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(UserSignedIn @event)
            => sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);

        public Task HandleAsync(UserSignedUp @event)
            => sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
    }
}
