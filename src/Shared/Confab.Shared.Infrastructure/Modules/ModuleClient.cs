using Confab.Shared.Abstraction.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry moduleRegistry;
        private readonly IModuleSerializer moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            this.moduleRegistry = moduleRegistry ?? throw new ArgumentNullException(nameof(moduleRegistry));
            this.moduleSerializer = moduleSerializer ?? throw new ArgumentNullException(nameof(moduleSerializer));
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = moduleRegistry.GetModuleBradcastRegistrations(key);

            var tasks = new List<Task>();
            foreach (var registration in registrations)
            {
                var receiverMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(registration.Action(receiverMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object value, Type type)
            => this.moduleSerializer.Deserialzie(this.moduleSerializer.Serialize(value), type);
    }
}
