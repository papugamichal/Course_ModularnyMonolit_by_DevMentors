using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.API.Controllers
{
    [ApiController]
    [Route(UsersModule.BasePath + "/[controller]")]
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
