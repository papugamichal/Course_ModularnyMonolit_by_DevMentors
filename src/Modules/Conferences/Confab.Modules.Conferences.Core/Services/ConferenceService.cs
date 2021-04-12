using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Messages.Events;
using Confab.Shared.Abstraction.Events;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository conferenceRepository;
        private readonly IHostRepository hostRepository;
        private readonly IConferenceDeletionPolicy conferenceDeletionPolicy;
        private readonly IEventDispatcher eventDispatcher;

        public ConferenceService(
            IConferenceRepository conferenceRepository,
            IHostRepository hostRepository,
            IConferenceDeletionPolicy conferenceDeletionPolicy,
            IEventDispatcher eventDispatcher)
        {
            this.conferenceRepository = conferenceRepository ?? throw new ArgumentNullException(nameof(conferenceRepository));
            this.hostRepository = hostRepository ?? throw new ArgumentNullException(nameof(hostRepository));
            this.conferenceDeletionPolicy = conferenceDeletionPolicy ?? throw new ArgumentNullException(nameof(conferenceDeletionPolicy));
            this.eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        public async Task AddAsync(ConferenceDto dto)
        {
            if (await this.hostRepository.GetAsync(dto.HostId) is null)
            {
                throw new HostNotFoundException(dto.Id);
            }

            dto.Id = Guid.NewGuid();
            var conference = Map<Conference>(dto);
            await this.conferenceRepository.AddAsync(conference);
            await this.eventDispatcher.PublishAsync(
                new ConferenceCreated(
                    conference.Id, conference.Name, conference.ParticipantsLimit,
                    conference.From, conference.To));
        }

        public async Task<IReadOnlyList<ConferenceDto>> BrowseAsync()
        {
            var conferences = await this.conferenceRepository.BrowseAsync();
            if (conferences is null)
            {
                return null;
            }

            return conferences.Select(x => Map<ConferenceDetailsDto>(x)).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            var confernce = await this.conferenceRepository.GetAsync(id);
            if (confernce is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            if (!await this.conferenceDeletionPolicy.CanDeleteAsync(confernce))
            {
                throw new CannotDeleteConferenceException(id);
            }

            await this.conferenceRepository.DeleteAsync(confernce);
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await this.conferenceRepository.GetAsync(id);
            if (conference is null)
            {
                return null;
            }

            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description = conference.Description;
            return dto;
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var confernce = this.conferenceRepository.GetAsync(dto.Id);
            if (confernce is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            var conference = Map<Conference>(dto);
            await this.conferenceRepository.UpdateAsync(conference);
        }

        internal static T Map<T>(ConferenceDto conference) where T : Conference, new()
            => new T
            {
                Id = conference.Id,
                HostId = conference.HostId,
                Name = conference.Name,
                Description = conference.Description,
                From = conference.From,
                To = conference.To,
                Location = conference.Location,
                LogoUrl = conference.LogoUrl,
                ParticipantsLimit = conference.ParticipantsLimit,
            };

        internal static T Map<T>(Conference conference) where T : ConferenceDto, new()
            => new T
            {
                Id = conference.Id,
                HostId = conference.HostId,
                Name = conference.Name,
                Description = conference.Description,
                From = conference.From,
                To = conference.To,
                Location = conference.Location,
                LogoUrl = conference.LogoUrl,
                ParticipantsLimit = conference.ParticipantsLimit,
            };
    }
}
