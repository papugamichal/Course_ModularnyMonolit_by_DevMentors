using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Confab.Modules.Users.Core.Services;
using Confab.Shared.Abstraction.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Users.API.Controllers
{
    [Authorize(Policy = UsersModule.Policy)]
    [Route(UsersModule.BasePath + "/[controller]")]
    internal class AccountController : BaseController
    {
        private readonly IIdentityService identityService;
        private readonly IContext context;

        public AccountController(
            IIdentityService identityService,
            IContext context)
        {
            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDto>> GetAsync()
        {
            return Ok(await this.identityService.GetAsync(this.context.Identity.Id));
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult> SignUpAsync([FromBody] SignUpDto dto)
        {
            await this.identityService.SignUpAsync(dto);
            return NoContent();
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<ActionResult> SignInAsync([FromBody] SignInDto dto)
        {
            return Ok(await this.identityService.SignInAsync(dto));
        }
    }
}
