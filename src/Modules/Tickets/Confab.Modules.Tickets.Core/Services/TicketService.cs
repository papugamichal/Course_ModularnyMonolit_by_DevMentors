using Confab.Modules.Tickets.Core.DTO;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Exceptions;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstraction.Time;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Services
{
    internal class TicketService : ITicketService
    {
        private readonly IClock clock;
        private readonly IConferenceRepository conferenceRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketSaleRepository ticketSaleRepository;
        private readonly ITicketGenerator ticketGenerator;
        private readonly ILogger<TicketService> logger;

        public TicketService(IClock clock, IConferenceRepository conferenceRepository,
            ITicketRepository ticketRepository, ITicketSaleRepository ticketSaleRepository, ITicketGenerator ticketGenerator,
            ILogger<TicketService> logger)
        {
            this.clock = clock;
            this.conferenceRepository = conferenceRepository;
            this.ticketRepository = ticketRepository;
            this.ticketSaleRepository = ticketSaleRepository;
            this.ticketGenerator = ticketGenerator;
            this.logger = logger;
        }

        public async Task PurchaseAsync(Guid conferenceId, Guid userId)
        {
            var conference = await this.conferenceRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(conferenceId);
            }

            var ticket = await this.ticketRepository.GetAsync(conferenceId, userId);
            if (ticket is not null)
            {
                throw new TicketAlreadyPurchasedException(conferenceId, userId);
            }

            var now = clock.CurrentDate();
            var ticketSale = await ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, now);
            if (ticketSale is null)
            {
                throw new TicketSaleUnavailableException(conferenceId);
            }

            if (ticketSale.Amount.HasValue)
            {
                await PurchaseAvailableAsync(ticketSale, userId, ticketSale.Price);
                return;
            }

            ticket = this.ticketGenerator.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
            ticket.Purchase(userId, clock.CurrentDate(), ticketSale.Price);
            await ticketRepository.AddAsync(ticket);
            logger.LogInformation($"Ticket with ID: '{ticket.Id}' was generated for the conference: " +
                                   $"'{conferenceId}' by user: '{userId}'.");
        }

        private async Task PurchaseAvailableAsync(TicketSale ticketSale, Guid userId, decimal? price)
        {
            var conferenceId = ticketSale.ConferenceId;
            var ticket = ticketSale.Tickets.Where(x => x.UserId is null).OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
            if (ticket is null)
            {
                throw new TicketsUnavailableException(conferenceId);
            }
             
            ticket.Purchase(userId, clock.CurrentDate(), price);
            await ticketRepository.UpdateAsync(ticket);
            logger.LogInformation($"Ticket with ID: '{ticket.Id}' was purchased for the conference: " +
                                   $"'{conferenceId}' by user: '{userId}'.");
        }

        public async Task<IReadOnlyList<TicketDto>> GetForUserAsync(Guid userId)
        {
            var tickets = await ticketRepository.GetForUserAsync(userId);

            return tickets.Select(x => new TicketDto(x.Code, x.Price, x.PurchasedAt.Value,
                    new ConferenceDto(x.ConferenceId, x.Conference.Name)))
                .OrderBy(x => x.PurchasedAt)
                .ToList();
        }
    }
}
