using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.API.Controllers
{
    [Route(TicketsModule.BasePath + "/[controller]")]
    internal class SalesController : BaseController
    {
        private readonly ITicketSaleService ticketSaleService;

        public SalesController(
            ITicketSaleService ticketSaleService
            )
        {
            this.ticketSaleService = ticketSaleService;
        }

        [HttpGet("conferneces/{conferenceId:guid}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<TicketSaleInfoDto>>> GetAll(Guid conferenceId)
        {
            return OkOrNotFound(await this.ticketSaleService.GetAllAsync(conferenceId));
        }

        [HttpGet("conferneces/{conferenceId:guid}/current")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TicketSaleInfoDto>> GetCurrent(Guid conferenceId)
        {
            return OkOrNotFound(await this.ticketSaleService.GetCurrentAsync(conferenceId));
        }
        
        [Authorize(TicketsModule.TicketsPolicy)]
        [HttpPost("conferneces/{conferenceId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<TicketSaleInfoDto>> Post(Guid conferenceId, TicketSaleDto dto)
        {
            dto.ConferenceId = conferenceId;
            await this.ticketSaleService.AddAsync(dto);
            return NoContent();
        }
    }
}
