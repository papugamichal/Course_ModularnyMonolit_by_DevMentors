using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Confab.Shared.Abstraction.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
        IEnumerable<string> Policies { get; }
    }
}
