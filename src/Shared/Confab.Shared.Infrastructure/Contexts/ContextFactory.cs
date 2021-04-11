using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Contexts;
using Microsoft.AspNetCore.Http;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal class ContextFactory : IContextFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IContext Create()
        {
            var httpContext = this.httpContextAccessor.HttpContext;
            return httpContext is null ? Context.Empty : new Context(httpContext);
        }
    }
}
