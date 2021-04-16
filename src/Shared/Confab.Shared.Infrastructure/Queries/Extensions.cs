using Confab.Shared.Abstraction.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                ;

            return services;
        }
    }
}
