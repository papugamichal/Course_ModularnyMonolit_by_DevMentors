﻿using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Services
{
    internal class AppInitializer : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<AppInitializer> logger;

        public AppInitializer(
            IServiceProvider serviceProvider,
            ILogger<AppInitializer> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext))
                ;

            using var scope = this.serviceProvider.CreateScope();
            foreach(var dbContexType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContexType) as DbContext;
                if (dbContext is null)
                {
                    continue;
                }

                await dbContext.Database.MigrateAsync(cancellationToken); 
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
