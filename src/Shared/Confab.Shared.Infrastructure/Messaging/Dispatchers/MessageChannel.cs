using Confab.Shared.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal sealed class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> messages = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => messages.Reader;

        public ChannelWriter<IMessage> Writer => messages.Writer;
    }
}
