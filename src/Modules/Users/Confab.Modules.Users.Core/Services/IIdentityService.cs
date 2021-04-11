using System;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Confab.Shared.Abstraction.Auth;

namespace Confab.Modules.Users.Core.Services
{
    public interface IIdentityService
    {
        Task<AccountDto> GetAsync(Guid id);
        Task<JsonWebToken> SignInAsync(SignInDto dto);
        Task SignUpAsync(SignUpDto dto);
    }
}