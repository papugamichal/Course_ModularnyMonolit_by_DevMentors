using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Confab.Modules.Speakers.Core.Services;
using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Modules.Speakers.API.Controllers
{
    [Route(SpeakersModule.BasePath + "/[controller]")]
    internal class SpeakersController : BaseController
    {
        private readonly ISpeakerService spekaerService;

        public SpeakersController(
            ISpeakerService service)
        {
            this.spekaerService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> Get()
        {
            return Ok(await this.spekaerService.BrowseAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SpeakerDto>> Get(Guid id)
        {
            return OkOrNotFound(await this.spekaerService.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] SpeakerDto dto)
        {
            await this.spekaerService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] SpeakerDto dto)
        {
            dto.Id = id;
            await this.spekaerService.UpdateAsync(dto);
            return NoContent();
        }
    }
}
