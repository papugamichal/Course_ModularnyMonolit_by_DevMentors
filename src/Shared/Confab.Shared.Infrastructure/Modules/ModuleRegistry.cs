using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBradcastRegistration> bradcastRegistrations = new();
    
        private readonly Dictionary<string, ModuleRequestRegistration> requestRegistrations = new();

        public void AddBroadcastAction(Type requestType, Func<object, Task> action)
        {
            if (string.IsNullOrWhiteSpace(requestType.Namespace))
            {
                throw new InvalidOperationException("Missing namespace");
            }

            var registration = new ModuleBradcastRegistration(requestType, action);
            this.bradcastRegistrations.Add(registration);
        }

        public void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action)
        {
            if (path is null) 
            {
                throw new InvalidOperationException("Request path cannot be null");
            }
            var registration = new ModuleRequestRegistration(requestType, responseType, action);
            requestRegistrations.Add(path, registration);
        }

        public IEnumerable<ModuleBradcastRegistration> GetModuleBradcastRegistrations(string key)
        {
            return bradcastRegistrations.Where(e => e.Key == key);
        }

        public ModuleRequestRegistration GetRequestRegistrations(string path)
            => requestRegistrations.TryGetValue(path, out var registration) ? registration : null;
    }
}
