using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.DTO
{

    public record TicketDto(string Code, decimal? Price, DateTime PurchasedAt, ConferenceDto Conference);
}
