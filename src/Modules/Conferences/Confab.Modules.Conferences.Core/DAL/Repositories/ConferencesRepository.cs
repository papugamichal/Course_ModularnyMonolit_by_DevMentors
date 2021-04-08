using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class ConferencesRepository : IConferenceRepository
    {
        private readonly ConferencesDbContext dbContext;
        private readonly DbSet<Conference> conferences;

        public ConferencesRepository(ConferencesDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.conferences = dbContext.Conferences;
        }

        public async Task AddAsync(Conference conference)
        {
            await this.conferences.AddAsync(conference);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Conference>> BrowseAsync()
            => await this.conferences.Include(e => e.Host).ToListAsync();

        public async Task DeleteAsync(Conference conference)
        {
            this.conferences.Remove(conference);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Conference> GetAsync(Guid id)
            => await this.conferences.Include(e => e.Host).SingleOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Conference conference)
        {
            this.conferences.Update(conference);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
