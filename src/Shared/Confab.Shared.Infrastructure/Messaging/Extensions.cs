using Confab.Shared.Abstraction.Messaging;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
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
        private const string SectionName = "Messaging";

        public static IServiceCollection AddMessaging(
            this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
            services.AddSingleton(messagingOptions);

            if (messagingOptions.UseBackgroundEventDispatcher)
            {
                services.AddHostedService<AsyncMessageBackgroundDispatcher>();
            }

            return services;
        } 
    }
}
