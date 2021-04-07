using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryConferenceRepository : IConferenceRepository
    {
        // Not thread-safe, use Concurent collections
        private readonly List<Conference> conferences = new();
        
        public Task AddAsync(Conference conference)
        {
            this.conferences.Add(conference);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Conference>> BrowseAsync()
        {
            await Task.CompletedTask;
            return this.conferences;
        }

        public Task DeleteAsync(Conference conference)
        {
            this.conferences.Remove(conference);
            return Task.CompletedTask;
        }

        public Task<Conference> GetAsync(Guid id)
            => Task.FromResult(this.conferences.SingleOrDefault(x => x.Id == id));

        public Task UpdateAsync(Conference conference)
        {
            return Task.CompletedTask;
        }
    }
}
