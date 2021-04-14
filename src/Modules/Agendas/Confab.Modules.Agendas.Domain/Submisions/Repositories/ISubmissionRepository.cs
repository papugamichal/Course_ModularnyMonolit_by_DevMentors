using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Shared.Abstraction.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Repositories
{
    interface ISubmissionRepository
    {
        Task<Submission> GetAsync(AggregateId id);
        Task AddAsync(Submission submision);
        Task UpdateAsync(Submission submision);
    }
}
