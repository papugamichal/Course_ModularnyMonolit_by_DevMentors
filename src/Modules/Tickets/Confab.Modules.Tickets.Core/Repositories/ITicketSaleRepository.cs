﻿using Confab.Modules.Tickets.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface ITicketSaleRepository
    {
        Task<TicketSale> GetAsync(Guid id);
        Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now);
        Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId);
        Task AddAsync(TicketSale ticketSale);
        Task UpdateAsync(TicketSale ticketSale);
        Task DeleteAsync(TicketSale ticketSale);
    }
}
