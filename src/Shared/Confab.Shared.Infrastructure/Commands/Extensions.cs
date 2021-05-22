using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstraction.Commands;
using Microsoft.Extensions.DependencyInjection;

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
                    .AddClasses(c => 
                        c.AssignableTo(typeof(ICommandHandler<>)).WithoutAttribute<DecoratorAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                ;

            return services;
        }
    }
}
