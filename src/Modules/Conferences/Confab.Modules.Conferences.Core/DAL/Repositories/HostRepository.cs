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
    internal class HostRepository : IHostRepository
    {
        private readonly ConferencesDbContext dbContext;
        private readonly DbSet<Host> hosts;

        public HostRepository(ConferencesDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.hosts = dbContext.Hosts;
        }

        public async Task AddAsync(Host host)
        {
            await this.hosts.AddAsync(host);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Host>> BrowseAsync()
            => await this.hosts.Include(e => e.Conferences).ToListAsync();

        public async Task DeleteAsync(Host host)
        {
            this.hosts.Remove(host);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<Host> GetAsync(Guid id)
            => await this.hosts.Include(e => e.Conferences).SingleOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Host host)
        {
            this.hosts.Update(host);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
