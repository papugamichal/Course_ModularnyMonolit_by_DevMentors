﻿using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.API.Controllers
{
    [ApiController] // np for Swagger
    [ProducesDefaultContentType]
    [Route(AgendasModule.BasePath + "/[controller]")]
    internal class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}
