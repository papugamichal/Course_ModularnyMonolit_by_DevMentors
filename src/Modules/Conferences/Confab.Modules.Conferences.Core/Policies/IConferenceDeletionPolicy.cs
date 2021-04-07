using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstraction;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal interface IConferenceDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Conference host);
    }

    internal class ConferenceDeletionPolicy : IConferenceDeletionPolicy
    {
        private readonly IClock clock;

        public ConferenceDeletionPolicy(IClock clock)
        {
            this.clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public async Task<bool> CanDeleteAsync(Conference conference)
        {
            //TODO: Check if there are any participants?
            var canDelete = this.clock.CurrentDate().AddDays(7) < conference.From.Date;
            return canDelete;
        }
    }
}
