using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleRequestRegistration
    {
        public ModuleRequestRegistration(Type receiverType, Type responseType, Func<object, Task<object>> action)
        {
            RequestType = receiverType;
            ResponseType = responseType;
            Action = action;
        }

        public Type RequestType { get; }
        public Type ResponseType { get; }
        public Func<object, Task<object>> Action { get; }
    }
}
