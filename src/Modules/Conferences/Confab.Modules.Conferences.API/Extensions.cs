using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Conferences.API
{
    internal static class Extensions
    {
        public static IServiceCollection AddConferences(this IServiceCollection services)
        {
            services
                .AddCore()
                ;

            return services;
        }
    }
}
