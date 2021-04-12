using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL.Repositories
{
    internal class TicketSaleRepository : ITicketSaleRepository
    {
        private readonly TicketsDbContext context;
        private readonly DbSet<TicketSale> ticketSales;

        public TicketSaleRepository(TicketsDbContext context)
        {
            this.context = context;
            this.ticketSales = this.context.TicketSales;
        }

        public Task<TicketSale> GetAsync(Guid id)
            => this.ticketSales
                .Include(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.Id == id);

        public Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now)
            => this.ticketSales
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.From)
                .Include(x => x.Tickets)
                .LastOrDefaultAsync(x => x.From <= now && x.To >= now);

        public async Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId)
            => await this.ticketSales
                .AsNoTracking()
                .Where(x => x.ConferenceId == conferenceId)
                .Include(x => x.Tickets)
                .ToListAsync();

        public async Task AddAsync(TicketSale ticketSale)
        {
            await this.ticketSales.AddAsync(ticketSale);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketSale ticketSale)
        {
            this.ticketSales.Update(ticketSale);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketSale ticketSale)
        {
            this.ticketSales.Remove(ticketSale);
            await this.context.SaveChangesAsync();
        }
    }
}