using Confab.Shared.Abstraction.Messaging;
using Confab.Shared.Abstraction.Modules;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class AsyncMessageBackgroundDispatcher : BackgroundService
    {
        private readonly IMessageChannel messageChannel;
        private readonly IModuleClient moduleClient;
        private readonly ILogger<AsyncMessageBackgroundDispatcher> logger;

        public AsyncMessageBackgroundDispatcher(
            IMessageChannel messageChannel,
            IModuleClient moduleClient,
            ILogger<AsyncMessageBackgroundDispatcher> logger)
        {
            this.messageChannel = messageChannel ?? throw new ArgumentNullException(nameof(messageChannel));
            this.moduleClient = moduleClient ?? throw new ArgumentNullException(nameof(moduleClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Running the background event dispatcher.");

            await foreach (var message in this.messageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await this.moduleClient.PublishAsync(message);
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, exception.Message);
                }
            }

            this.logger.LogInformation("Finnished running the background event dispatcher.");
        }
    }
}
