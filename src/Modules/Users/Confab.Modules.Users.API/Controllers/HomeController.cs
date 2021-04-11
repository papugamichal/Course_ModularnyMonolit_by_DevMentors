using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.API.Controllers
{
    [Route(UsersModule.BasePath)]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public Task<string> Get() => Task.FromResult("Users API");
    }
}
