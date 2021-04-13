using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleBradcastRegistration
    {
        public Type ReceiverType { get; }
        public Func<object, Task> Action { get; }
        public string Key => ReceiverType.Name;

        public ModuleBradcastRegistration(Type receiverType, Func<object, Task> action)
        {
            ReceiverType = receiverType;
            Action = action;
        }
    }
}
