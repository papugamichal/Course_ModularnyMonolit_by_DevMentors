using Confab.Modules.Users.Core;
using Confab.Shared.Abstraction.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Confab.Modules.Users.API
{
    public class UsersModule : IModule
    {
        public const string BasePath = "users-module";
        public const string Policy = "users";

        public string Name { get; } = "Users";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            Policy
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
