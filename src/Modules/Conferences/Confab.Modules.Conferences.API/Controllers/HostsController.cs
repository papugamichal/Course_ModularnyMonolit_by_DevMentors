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
    internal class HostsController : BaseController
    {
        private readonly IHostService hostService;

        public HostsController(
            IHostService hostService)
        {
            this.hostService = hostService ?? throw new ArgumentNullException(nameof(hostService));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HostDetailsDto>> GetAsync(Guid id)
            => OkOrNotFound(await this.hostService.GetAsync(id));

        [HttpGet]
        public async Task<ActionResult<HostDetailsDto>> BrowseAsync()
            => Ok(await this.hostService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(HostDto dto)
        {
            await this.hostService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, HostDetailsDto dto)
        {
            dto.Id = id;
            await this.hostService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id)
        {
            await this.hostService.DeleteAsync(id);
            return NoContent();
        }
    }
}