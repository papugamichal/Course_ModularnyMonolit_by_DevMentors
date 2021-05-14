using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Policies;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstraction.Events;

namespace Confab.Modules.Attendances.Application.Events.External.Handlers
{
    internal sealed class AgendaItemAssignedToAgendaSlotHandler : IEventHandler<AgendaItemAssignedToAgendaSlot>
    {
        private readonly IAttendableEventsRepository attendableEventsRepository;
        private readonly IAgendasApiClient agendasApiClient;
        private readonly ISlotPolicyFactory slotPolicyFactory;

        public AgendaItemAssignedToAgendaSlotHandler(
            IAttendableEventsRepository attendableEventsRepository,
            IAgendasApiClient agendasApiClient,
            ISlotPolicyFactory slotPolicyFactory)
        {
            this.attendableEventsRepository = attendableEventsRepository;
            this.agendasApiClient = agendasApiClient;
            this.slotPolicyFactory = slotPolicyFactory;
        }

        public async Task HandleAsync(AgendaItemAssignedToAgendaSlot @event)
        {
            var attendableEvent = await attendableEventsRepository.GetAsync(@event.AgendaItemId);
            if (attendableEvent is not null)
            {
                return;
            }

            var slot = await agendasApiClient.GetRegularAgendaSlotAsync(@event.AgendaItemId);
            if (slot is null)
            {
                throw new AgendaItemNotFoundException(@event.AgendaItemId);
            }

            if (!slot.ParticipantsLimit.HasValue)
            {
                return;
            }

            attendableEvent = new Domain.Entities.AttendableEvent(@event.Id, slot.AgendaItem.ConferenceId, slot.From, slot.To);
            var slotPolicy = slotPolicyFactory.Get(slot.AgendaItem.Tags.ToArray());
            var slots = slotPolicy.Generate(slot.ParticipantsLimit.Value);
            attendableEvent.AddSlots(slots);
            await attendableEventsRepository.AddAsync(attendableEvent);
        }
    }
}
