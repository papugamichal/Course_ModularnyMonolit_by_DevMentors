using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class TicketRepository : ITicketRepository
    {
        private readonly TicketsDbContext context;
        private readonly DbSet<Ticket> tickets;

        public TicketRepository(TicketsDbContext context)
        {
            this.context = context;
            this.tickets = this.context.Tickets;
        }

        public Task<Ticket> GetAsync(Guid conferenceId, Guid userId)
            => this.tickets.SingleOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);

        public Task<int> CountForConferenceAsync(Guid conferenceId)
            => this.tickets.CountAsync(x => x.ConferenceId == conferenceId);

        public async Task<IReadOnlyList<Ticket>> GetForUserAsync(Guid userId)
            => await this.tickets.Include(x => x.Conference).Where(x => x.UserId == userId).ToListAsync();

        public Task<Ticket> GetAsync(string code)
            => this.tickets.SingleOrDefaultAsync(x => x.Code == code);

        public async Task AddAsync(Ticket ticket)
        {
            await this.tickets.AddAsync(ticket);
            await this.context.SaveChangesAsync();
        }

        public async Task AddManyAsync(IEnumerable<Ticket> ticket)
        {
            await this.tickets.AddRangeAsync(ticket);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            this.tickets.Update(ticket);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            this.tickets.Remove(ticket);
            await this.context.SaveChangesAsync();
        }
    }
}