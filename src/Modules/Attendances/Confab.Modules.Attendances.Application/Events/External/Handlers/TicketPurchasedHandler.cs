using System;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstraction.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Attendances.Application.Events.External.Handlers
{
    internal sealed class TicketPurchasedHandler : IEventHandler<TicketPurchased>
    {
        private readonly IParticipantsRepository participantsRepository;
        private readonly ILogger<TicketPurchasedHandler> logger;

        public TicketPurchasedHandler(IParticipantsRepository participantsRepository, ILogger<TicketPurchasedHandler> logger)
        {
            this.participantsRepository = participantsRepository;
            this.logger = logger;
        }


        public async Task HandleAsync(TicketPurchased @event)
        {
            var paritcipant = await participantsRepository.GetAsync(@event.ConferenceId, @event.UserId);
            if (paritcipant is not null)
            {
                return;
            }

            paritcipant = new Domain.Entities.Participant(Guid.NewGuid(), @event.ConferenceId, @event.UserId);
            await participantsRepository.AddAsync(paritcipant);
            logger.LogInformation($"Added participant with ID: '{paritcipant.Id}' for conferences: '{paritcipant.ConferenceId}', user: {paritcipant.UserId}.");
        }
    }
}
