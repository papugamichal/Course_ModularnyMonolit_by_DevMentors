﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.API.Controllers
{
    [Route(BasePath + "/[controller]")]
    internal class ConferencesController : BaseController
    {
        private readonly IConferenceService conferenceService;

        public ConferencesController(
            IConferenceService conferenceService)
        {
            this.conferenceService = conferenceService ?? throw new ArgumentNullException(nameof(conferenceService));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConferenceDetailsDto>> Get(Guid id)
            => OkOrNotFound(await this.conferenceService.GetAsync(id));

        [HttpGet]
        public async Task<ActionResult<ConferenceDetailsDto>> BrowseAsync()
            => Ok(await this.conferenceService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] ConferenceDto dto)
        {
            await this.conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await this.conferenceService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id)
        {
            await this.conferenceService.DeleteAsync(id);
            return NoContent();
        }
    }
}