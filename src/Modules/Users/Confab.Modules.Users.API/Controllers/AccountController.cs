using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Confab.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.API.Controllers
{
    [Authorize(Policy = UsersModule.Policy)]
    [Route(UsersModule.BasePath + "/[controller]")]
    internal class AccountController : BaseController
    {
        private readonly IIdentityService identityService;

        public AccountController(
            IIdentityService identityService)
        {
            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDto>> GetAsync()
        {
            return Ok(await this.identityService.GetAsync(Guid.Parse(User.Identity.Name)));
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpDto dto)
        {
            await this.identityService.SignUpAsync(dto);
            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> SignInAsync([FromBody] SignInDto dto)
        {
            return Ok(await this.identityService.SignInAsync(dto));
        }
    }
}
