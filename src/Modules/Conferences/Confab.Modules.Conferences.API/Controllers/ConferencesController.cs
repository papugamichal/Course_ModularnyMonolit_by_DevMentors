using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.API.Controllers
{
    [Authorize(Policy = ConferencesModule.ConfenerencesPolicy)]
    [Route(ConferencesModule.BasePath + "/[controller]")]
    internal class ConferencesController : BaseController
    {
        private readonly IConferenceService conferenceService;

        public ConferencesController(
            IConferenceService conferenceService)
        {
            this.conferenceService = conferenceService ?? throw new ArgumentNullException(nameof(conferenceService));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ConferenceDetailsDto>> Get(Guid id)
            => OkOrNotFound(await this.conferenceService.GetAsync(id));

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ConferenceDetailsDto>> BrowseAsync()
            => Ok(await this.conferenceService.BrowseAsync());

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> AddAsync([FromBody] ConferenceDto dto)
        {
            await this.conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);

        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await this.conferenceService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await this.conferenceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
