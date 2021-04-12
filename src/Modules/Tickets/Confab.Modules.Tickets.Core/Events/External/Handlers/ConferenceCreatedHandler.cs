using Confab.Modules.Conferences.Messages.Events;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstraction.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Events.External.Handlers
{
    internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
    {
        private readonly IConferenceRepository conferenceRepository;
        private readonly ILogger<ConferenceCreatedHandler> logger;

        public ConferenceCreatedHandler(IConferenceRepository conferenceRepository,
            ILogger<ConferenceCreatedHandler> logger)
        {
            this.conferenceRepository = conferenceRepository;
            this.logger = logger;
        }

        public async Task HandleAsync(ConferenceCreated @event)
        {
            var conference = new Conference
            {
                Id = @event.ConferenceId,
                Name = @event.Name,
                ParticipantsLimit = @event.ParticipantsList,
                From = @event.From,
                To = @event.To
            };

            await this.conferenceRepository.AddAsync(conference);
            this.logger.LogInformation($"Added a conference with ID: '{@event.ConferenceId}'");
        }
    }
}
