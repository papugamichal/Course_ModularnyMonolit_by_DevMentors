using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public class ModuleSubscriber : IModuleSubscriber
    {
        private readonly IModuleRegistry moduleRegistry;
        private readonly IServiceProvider serviceProvider;

        public ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
        {
            this.moduleRegistry = moduleRegistry;
            this.serviceProvider = serviceProvider;
        }

        public IModuleSubscriber Subscribe<TRequest, TResponse>(string path, Func<TRequest, IServiceProvider, Task<TResponse>> action)
            where TRequest : class
            where TResponse : class
        {
            moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse), async request => await action((TRequest)request, serviceProvider));
            return this;
        }
    }
}
