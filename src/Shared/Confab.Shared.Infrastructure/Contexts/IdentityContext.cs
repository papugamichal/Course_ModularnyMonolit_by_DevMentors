using Confab.Shared.Abstraction.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
    {
        public bool IsAuthenticated { get; }
        public Guid Id { get; }
        public string Role { get; }
        public IDictionary<string, IEnumerable<string>> Claims { get; }

        public IdentityContext(ClaimsPrincipal principal)
        {
            this.IsAuthenticated = principal.Identity?.IsAuthenticated is true;
            this.Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
            this.Role = principal.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            this.Claims = principal.Claims.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
        }
    }
}
