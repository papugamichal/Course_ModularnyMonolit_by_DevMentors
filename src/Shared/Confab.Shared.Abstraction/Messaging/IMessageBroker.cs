using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Messaging
{
    public interface IMessageBroker 
    {
        Task PublishAsync(params IMessage[] messages);
    }
}
