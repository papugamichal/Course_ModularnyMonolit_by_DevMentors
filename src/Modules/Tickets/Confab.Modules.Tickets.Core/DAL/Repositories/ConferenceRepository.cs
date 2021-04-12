using System;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly TicketsDbContext context;
        private readonly DbSet<Conference> conferences;

        public ConferenceRepository(TicketsDbContext context)
        {
            this.context = context;
            this.conferences = this.context.Conferences;
        }

        public Task<Conference> GetAsync(Guid id) => conferences.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Conference conference)
        {
            await this.conferences.AddAsync(conference);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conference conference)
        {
            this.conferences.Update(conference);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conference conference)
        {
            this.conferences.Remove(conference);
            await this.context.SaveChangesAsync();
        }
    }
}