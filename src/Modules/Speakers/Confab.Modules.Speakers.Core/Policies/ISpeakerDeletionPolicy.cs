using Confab.Modules.Speakers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Speakers.Core.Policies
{
    internal interface ISpeakerDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Speaker speaker);
    }

    internal class SpeakerDeletionPolicy : ISpeakerDeletionPolicy
    {
        public SpeakerDeletionPolicy()
        {
        }

        public async Task<bool> CanDeleteAsync(Speaker speaker)
        {
            return true;
        }
    }
}
