using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Modules;

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

        public async Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class
        {
            var registration = moduleRegistry.GetRequestRegistrations(path);
            if (registration is null)
            {
                throw new InvalidOperationException($"No action has been defined for path: '{path}'");
            }

            var receiverRequest = TranslateType(request, registration.RequestType);
            var result = await registration.Action(receiverRequest);

            return result is null ? null : TranslateType<TResult>(result);
        }

        private object TranslateType(object value, Type type)
            => this.moduleSerializer.Deserialize(this.moduleSerializer.Serialize(value), type);

        private T TranslateType<T>(object value)
            => this.moduleSerializer.Deserialize<T>(this.moduleSerializer.Serialize(value));
    }
}
