using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.API.Controllers
{
    [ApiController] // np for Swagger
    [Route(BasePath + "[controller]")]
    internal class BaseController : ControllerBase
    {
        protected const string BasePath = "conferences-module";

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
