using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Shared.Abstraction.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Repositories
{
    public interface ISpeakerRepository
    {
        Task<bool> ExistsAsync(AggregateId id);
        Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids);
        Task AddAsync(Speaker speaker);
    }
}
