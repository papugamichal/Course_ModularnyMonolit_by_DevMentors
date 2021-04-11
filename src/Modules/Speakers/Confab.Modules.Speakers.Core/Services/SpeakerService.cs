using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Mappings;
using Confab.Modules.Speakers.Core.Policies;
using Confab.Modules.Speakers.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Services
{
    internal class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository speakerRepository;
        private readonly ISpeakerDeletionPolicy speakerDeletionPolicy;

        public SpeakerService(
            ISpeakerRepository speakerRepository,
            ISpeakerDeletionPolicy speakerDeletionPolicy)
        {
            this.speakerRepository = speakerRepository ?? throw new ArgumentNullException(nameof(speakerRepository));
            this.speakerDeletionPolicy = speakerDeletionPolicy ?? throw new ArgumentNullException(nameof(speakerDeletionPolicy));
        }

        public async Task AddAsync(SpeakerDto dto)
        {
            var alreadyExists = await this.speakerRepository.ExistsAsync(dto.Id);
            if (alreadyExists)
            {
                throw new SpeakerAlreadyExistsException(dto.Id);
            }

            //dto.Id = Guid.NewGuid();
            var speaker = dto.AsEntity();
            await this.speakerRepository.AddAsync(speaker);
        }

        public async Task<IReadOnlyList<SpeakerDto>> BrowseAsync()
        {
            var speakers = await this.speakerRepository.BrowseAsync();
            if (speakers is null)
            {
                return new List<SpeakerDto>();
            }

            return speakers.Select(x => x.AsDto()).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            var speaker = await this.speakerRepository.GetAsync(id);
            if (speaker is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            var canDelete = await this.speakerDeletionPolicy.CanDeleteAsync(speaker);
            if (!canDelete)
            {
                throw new CannotDeleteSpeakerException(id);
            }

            await this.speakerRepository.DeleteAsync(speaker);
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var speaker = await this.speakerRepository.GetAsync(id);
            if (speaker is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            return speaker.AsDto();
        }

        public async Task UpdateAsync(SpeakerDto dto)
        {
            var alreadyExists = await this.speakerRepository.ExistsAsync(dto.Id);
            if (!alreadyExists)
            {
                throw new SpeakerNotFoundException(dto.Id);
            }

            var updatedSpeaker = dto.AsEntity();
            await this.speakerRepository.UpdateAsync(updatedSpeaker);
        }
    }
}
