using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBradcastRegistration> GetModuleBradcastRegistrations(string key);
        void AddBroadcastAction(Type requestType, Func<object, Task> action);
        void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action);
        ModuleRequestRegistration GetRequestRegistrations(string path);
    }
}
