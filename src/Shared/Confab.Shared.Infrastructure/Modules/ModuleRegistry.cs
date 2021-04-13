using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBradcastRegistration> bradcastRegistrations = new();

        public void AddBroadcastAction(Type requestType, Func<object, Task> action)
        {
            if (string.IsNullOrWhiteSpace(requestType.Namespace))
            {
                throw new InvalidOperationException("Missing namespace");
            }

            var registration = new ModuleBradcastRegistration(requestType, action);
            this.bradcastRegistrations.Add(registration);
        }

        public IEnumerable<ModuleBradcastRegistration> GetModuleBradcastRegistrations(string key)
        {
            return bradcastRegistrations.Where(e => e.Key == key);
        }
    }
}
