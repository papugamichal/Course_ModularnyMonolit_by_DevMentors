using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal interface IHostRepository
    {
        Task<Host> GetAsync(Guid id);
        Task<IReadOnlyList<Host>> BrowseAsync();
        Task AddAsync(Host host);
        Task UpdateAsync(Host host);
        Task DeleteAsync(Host host);
    }

    internal class InMemoryHostRepository : IHostRepository
    {
        // Not thread-safe, use Concurent collections
        private readonly List<Host> hosts = new List<Host>();
        
        public Task AddAsync(Host host)
        {
            this.hosts.Add(host);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Host>> BrowseAsync()
        {
            await Task.CompletedTask;
            return this.hosts;
        }

        public Task DeleteAsync(Host host)
        {
            this.hosts.Remove(host);
            return Task.CompletedTask;
        }

        public Task<Host> GetAsync(Guid id)
            => Task.FromResult(this.hosts.SingleOrDefault(x => x.Id == id));

        public Task UpdateAsync(Host host)
        {
            return Task.CompletedTask;
        }
    }
}
