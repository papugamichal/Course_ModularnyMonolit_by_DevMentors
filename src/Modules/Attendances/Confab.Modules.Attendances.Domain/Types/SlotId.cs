using System;
using Confab.Shared.Abstraction.Kernel.Types;

namespace Confab.Modules.Attendances.Domain.Types
{
    public class SlotId : TypeId
    {
        public SlotId(Guid value) : base(value)
        {
        }
        
        public static implicit operator SlotId(Guid id) => new(id);
    }
}