using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Mappings
{
    internal static class Extensions
    {
        internal static SpeakerDto AsDto(this Speaker speaker)
        {
            var dto = new SpeakerDto()
            {
                Id = speaker.Id,
                Email = speaker.Email,
                FullName = speaker.FullName,
                Bio = speaker.Bio,
                AvatralUrl = speaker.AvatralUrl
            };
            return dto;
        }

        internal static Speaker AsEntity(this SpeakerDto dto)
        {
            var speaker = new Speaker()
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.FullName,
                Bio = dto.Bio,
                AvatralUrl = dto.AvatralUrl
            };
            return speaker;
        }
    }
}
