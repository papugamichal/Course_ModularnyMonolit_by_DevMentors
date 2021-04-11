﻿using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.API.Controllers
{
    [ApiController]
    [Route(SpeakersModule.BasePath + "/[controller]")]
    internal class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}
