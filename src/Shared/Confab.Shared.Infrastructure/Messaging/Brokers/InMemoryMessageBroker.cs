using Confab.Shared.Abstraction.Messaging;
using Confab.Shared.Abstraction.Modules;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient moduleClient;
        private readonly IAsyncMessageDispatcher asyncMessageDispatcher;
        private readonly MessagingOptions messagingOptions;

        public InMemoryMessageBroker(
            IModuleClient moduleClient,
            IAsyncMessageDispatcher asyncMessageDispatcher,
            MessagingOptions messagingOptions
            )
        {
            this.moduleClient = moduleClient ?? throw new ArgumentNullException(nameof(moduleClient));
            this.asyncMessageDispatcher = asyncMessageDispatcher;
            this.messagingOptions = messagingOptions;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var task = new List<Task>();
            foreach (var message in messages)
            {
                if (this.messagingOptions.UseBackgroundEventDispatcher)
                {
                    await this.asyncMessageDispatcher.PublicAsync(message);
                    continue;
                }

                task.Add(this.moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(task);
        }
    }
}
