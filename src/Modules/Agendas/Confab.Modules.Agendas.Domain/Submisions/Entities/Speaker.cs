using Confab.Shared.Abstraction.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Entities
{
    public class Speaker : AggregateRoot
    {
        public Speaker(AggregateId id, string fullName)
        {
            FullName = fullName;
            Id = id;
        }

        public string FullName { get; init; }

        public static Speaker Create(Guid id, string fullname)
            => new Speaker(id, fullname);
    }
}
