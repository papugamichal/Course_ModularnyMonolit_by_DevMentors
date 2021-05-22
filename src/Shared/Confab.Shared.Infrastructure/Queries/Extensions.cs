using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstraction.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services
                .AddSingleton<IQueryDispatcher, QueryDispatcher>()
                ;

            services
                .Scan(x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)).WithoutAttribute<DecoratorAttribute>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                ;

            return services;
        }
    }
}
