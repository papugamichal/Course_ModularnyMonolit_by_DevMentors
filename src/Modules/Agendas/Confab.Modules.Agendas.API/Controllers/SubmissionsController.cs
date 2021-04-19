using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Shared.Abstraction.Commands;
using Confab.Shared.Abstraction.Queries;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;

namespace Confab.Modules.Agendas.API.Controllers
{
    [Route(AgendasModule.BasePath + "/[controller]")]
    internal class SubmissionsController : BaseController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public SubmissionsController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            this.queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubmissionDto>> Get(Guid id)
            => OkOrNotFound(await this.queryDispatcher.QueryAsync(new GetSubmission() { Id = id }));

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
