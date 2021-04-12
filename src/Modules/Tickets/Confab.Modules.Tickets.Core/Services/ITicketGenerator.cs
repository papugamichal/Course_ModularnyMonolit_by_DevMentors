using Confab.Modules.Tickets.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Services
{
    internal interface ITicketGenerator
    {
        Ticket Generate(Guid conferenceId, Guid userId, decimal? price);
    }

    internal class TicketGenerator : ITicketGenerator
    {
        public Ticket Generate(Guid conferenceId, Guid userId, decimal? price)
        {
            throw new NotImplementedException();
        }
    }
}
