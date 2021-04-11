using System;
using System.Collections.Generic;

namespace Confab.Shared.Abstraction.Contexts
{
    public interface IIdentityContext
    {
        bool IsAuthenticated { get; }
        Guid Id { get; }
        string Role { get; }
        IDictionary<string, IEnumerable<string>> Claims { get; }
    }
}
