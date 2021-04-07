using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task<Conference> GetAsync(Guid id);
        Task<IReadOnlyList<Conference>> BrowseAsync();
        Task AddAsync(Conference confernce);
        Task UpdateAsync(Conference confernce);
        Task DeleteAsync(Conference confernce);
    }
}
