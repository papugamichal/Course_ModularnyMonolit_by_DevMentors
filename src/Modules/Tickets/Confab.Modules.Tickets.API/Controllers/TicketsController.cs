using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Services;
using Confab.Shared.Abstraction.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Tickets.API.Controllers
{
    [Authorize]
    [Route(TicketsModule.BasePath + "/[controller]")]
    internal class TicketsController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly IContext context;

        public TicketsController(
            ITicketService ticketService,
            IContext context
            )
        {
            this.ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IReadOnlyList<TicketDto>>> Get()
        {
            return OkOrNotFound(await this.ticketService.GetForUserAsync(this.context.Identity.Id));
        }

        [HttpPost("conferneces/{conferenceId:guid}/purchase")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Purchase(Guid conferenceId)
        {
            await this.ticketService.PurchaseAsync(conferenceId, this.context.Identity.Id);
            return NoContent();
        }
    }
}
