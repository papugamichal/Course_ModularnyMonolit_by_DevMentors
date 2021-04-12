using Confab.Modules.Tickets.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Services
{
    internal interface ITicketSaleService
    {
        Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid confernenceId);
        Task<TicketSaleInfoDto> GetCurrentAsync(Guid confernenceId);
        Task AddAsync(TicketSaleDto dto);
    }
}
