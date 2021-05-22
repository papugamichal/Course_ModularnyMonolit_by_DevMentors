using System;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres.Decorators
{
    [Decorator]
    internal class TransactionalCommandDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> handler;
        private readonly UnitOfWorkTypeRegistry unitOfWorkTypeRegistry;
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWork unitOfWork;

        public TransactionalCommandDecorator(ICommandHandler<T> commandHandler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
        {
            this.handler = commandHandler;
            this.unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
            this.serviceProvider = serviceProvider;
        }

        public async Task HandleAsync(T command)
        {
            var unitOfWorkType = unitOfWorkTypeRegistry.Resolve<T>();
            if (unitOfWorkType is null)
            {
                await handler.HandleAsync(command);
                return;
            }

            var unitOfWork = (IUnitOfWork) serviceProvider.GetRequiredService(unitOfWorkType);
            await unitOfWork.ExcuteAsync(() => handler.HandleAsync(command));
        }
    }
}
