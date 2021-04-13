using Confab.Shared.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublicAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}
