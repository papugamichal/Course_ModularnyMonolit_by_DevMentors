﻿using Confab.Modules.Tickets.Core.DAL;
using Confab.Modules.Tickets.Core.DAL.Repositories;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Modules.Tickets.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Tickets.API")]
namespace Confab.Modules.Tickets.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(
            this IServiceCollection services)
        {
            return services
                .AddPostgres<TicketsDbContext>()
                .AddScoped<ITicketService, TicketService>()
                .AddScoped<ITicketSaleService, TicketSaleService>()
                .AddScoped<IConferenceRepository, ConferenceRepository>()
                .AddScoped<ITicketRepository, TicketRepository>()
                .AddScoped<ITicketSaleRepository, TicketSaleRepository>()
                .AddSingleton<ITicketGenerator, TicketGenerator>()
                ;
        }
    }
}