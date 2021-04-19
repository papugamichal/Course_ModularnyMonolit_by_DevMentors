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

        // To satisfy EF Core many-to-many relation
        public IEnumerable<Submission> Submisions => submisions;

        private ICollection<Submission> submisions;

        public static Speaker Create(Guid id, string fullname)
            => new Speaker(id, fullname);
    }
}
