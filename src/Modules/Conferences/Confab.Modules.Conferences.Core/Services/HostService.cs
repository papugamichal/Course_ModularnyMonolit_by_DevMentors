using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class HostService : IHostService
    {
        private readonly IHostRepository hostRepository;
        private readonly IHostDeletionPolicy hostDeletionPolicy;

        public HostService(
            IHostRepository hostRepository,
            IHostDeletionPolicy hostDeletionPolicy)
        {
            this.hostRepository = hostRepository ?? throw new ArgumentNullException(nameof(hostRepository));
            this.hostDeletionPolicy = hostDeletionPolicy ?? throw new ArgumentNullException(nameof(hostDeletionPolicy));
        }

        public async Task AddAsync(HostDto dto)
        {
            dto.Id = Guid.NewGuid();
            await this.hostRepository.AddAsync(new Host
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            });
        }

        public async Task<HostDetailsDto> GetAsync(Guid id)
        {
            var host = await this.hostRepository.GetAsync(id);
            if (host is null)
            {
                return null;
            }
            
            var dto = Map<HostDetailsDto>(host);
            dto.Conferences = host.Conferences.Select(x => Map<ConferenceDto>(x)).ToList();
            return dto;
        }

        public async Task<IReadOnlyList<HostDto>> BrowseAsync()
        {
            var hosts = await this.hostRepository.BrowseAsync();
            if (hosts is null)
            {
                return null;
            }

            return hosts.Select(x => Map<HostDto>(x)).ToList();
        }

        public async Task UpdateAsync(HostDetailsDto dto)
        {
            var host = await this.hostRepository.GetAsync(dto.Id);
            if (host is null)
            {
                throw new HostNotFoundException(dto.Id);
            }

            host.Name = dto.Name;
            host.Description = dto.Description;
            
            await this.hostRepository.UpdateAsync(host);
        }

        public async Task DeleteAsync(Guid id)
        {
            var host = await this.hostRepository.GetAsync(id);
            if (host is null)
            {
                throw new HostNotFoundException(id);
            }

            if (await this.hostDeletionPolicy.CanDeleteAsync(host) is false)
            {
                throw new CannotDeleteHostException(host.Id);
            }


            await this.hostRepository.DeleteAsync(host);
        }

        private static T Map<T>(Host host) where T : HostDto, new()
            => new T
            {
                Id = host.Id,
                Name = host.Name,
                Description = host.Description
            };

        private static T Map<T>(Conference conference) where T : ConferenceDto, new()
            => new T
            {
                Id = conference.Id,
                HostId = conference.HostId,
                HostName = conference.Host.Name,
                From = conference.From,
                To = conference.To,
                Location = conference.Location,
                LogoUrl= conference.LogoUrl,
                ParticipantsLimit = conference.ParticipantsLimit,
                Name = conference.Name,
                Description = conference.Description
            };
    }
}

