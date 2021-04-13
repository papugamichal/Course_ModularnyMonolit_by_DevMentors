using Confab.Shared.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private readonly IMessageChannel messageChannel;

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
        {
            this.messageChannel = messageChannel ?? throw new ArgumentNullException(nameof(messageChannel));
        }

        public async Task PublicAsync<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            await this.messageChannel.Writer.WriteAsync(message);
        }
    }
}
