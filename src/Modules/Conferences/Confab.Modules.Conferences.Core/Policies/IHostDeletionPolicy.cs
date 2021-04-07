using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal interface IHostDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Host host);
    }

    internal class HostDeletionPolicy : IHostDeletionPolicy
    {
        private readonly IConferenceDeletionPolicy conferenceDeletionPolicy;

        public HostDeletionPolicy(IConferenceDeletionPolicy conferenceDeletionPolicy)
        {
            this.conferenceDeletionPolicy = conferenceDeletionPolicy;
        }

        public async Task<bool> CanDeleteAsync(Host host)
        {
            if (host.Conferences is null || !host.Conferences.Any())
            {
                return true;
            }

            foreach(var conference in host.Conferences)
            {
                if (await this.conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
