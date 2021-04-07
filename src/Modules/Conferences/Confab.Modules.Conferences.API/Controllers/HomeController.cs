using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.API.Controllers
{
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Conferences API";
    }
}
