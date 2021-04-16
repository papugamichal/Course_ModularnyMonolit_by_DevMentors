using Confab.Shared.Abstraction.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Commands
{
    internal static class Extensions
    {
        public static IServiceCollection AddCommands(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services
                .AddSingleton<ICommandDispatcher, CommandDispatcher>()
                ;

            services
                .Scan(x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                ;

            return services;
        }
    }
}
