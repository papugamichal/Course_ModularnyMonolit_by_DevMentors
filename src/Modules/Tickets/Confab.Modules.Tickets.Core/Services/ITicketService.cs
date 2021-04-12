using Confab.Modules.Tickets.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Services
{
    internal interface ITicketService
    {
        Task<IReadOnlyList<TicketDto>> GetForUserAsync(Guid userId);
        Task PurchaseAsync(Guid conferenceId, Guid id);
    }
}
