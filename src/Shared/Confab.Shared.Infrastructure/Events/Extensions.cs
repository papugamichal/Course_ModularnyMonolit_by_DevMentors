﻿using Confab.Shared.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services
                .AddSingleton<IEventDispatcher, EventDispatcher>()
                ;

            services
                .Scan(x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                ;

            return services;
        }
    }
}
