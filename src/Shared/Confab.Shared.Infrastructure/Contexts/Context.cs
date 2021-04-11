using Confab.Shared.Abstraction.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Contexts
{
    internal class Context : IContext
    {
        private readonly HttpContext httpContext;
        public string RequestId { get; }
        public string TraceId { get; }
        public IIdentityContext Identity { get; }

        internal Context()
        {
        }

        public Context(HttpContext httpContext) : this(httpContext.TraceIdentifier, new IdentityContext(httpContext.User))
        {
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public Context(string traceId, IIdentityContext identity)
        {
            this.TraceId = traceId;
            this.Identity = identity;
        }

        public static IContext Empty => new Context();
    }
}
