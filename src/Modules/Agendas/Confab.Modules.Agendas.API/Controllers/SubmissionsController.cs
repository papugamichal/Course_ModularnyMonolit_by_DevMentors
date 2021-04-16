using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.API.Controllers
{
    [Route(AgendasModule.BasePath + "/[controller]")]
    internal class SubmissionsController : BaseController
    {
        private readonly ICommandDispatcher commandDispatcher;

        public SubmissionsController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await this.commandDispatcher.SendAsync(command);
            return CreatedAtAction("Get", new { id = command.Id }, null);
        }

        [HttpPut("{id:guid}/approve")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await this.commandDispatcher.SendAsync(new ApproveSubmission(id));
            return NoContent();
        }

        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await this.commandDispatcher.SendAsync(new RejectSubmission(id));
            return NoContent();
        }
    }
}
