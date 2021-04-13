using Confab.Shared.Abstraction.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(
            this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            return services;
        } 
    }
}
