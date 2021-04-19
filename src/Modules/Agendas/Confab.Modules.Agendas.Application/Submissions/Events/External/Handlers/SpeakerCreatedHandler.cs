using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Shared.Abstraction.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External.Handlers
{
    public sealed class SpeakerCreatedHandler : IEventHandler<SpeakerCreated>
    {
        private readonly ISpeakerRepository speakerRepository;

        public SpeakerCreatedHandler(ISpeakerRepository speakerRepository)
        {
            this.speakerRepository = speakerRepository;
        }

        public async Task HandleAsync(SpeakerCreated @event)
        {
            if (await this.speakerRepository.ExistsAsync(@event.Id))
            {
                return;
            }

            var speaker = Speaker.Create(@event.Id, @event.FullName);
            await this.speakerRepository.AddAsync(speaker);
        }
    }
}
