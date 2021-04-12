﻿using Confab.Modules.Tickets.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task<Conference> GetAsync(Guid id);
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
        Task DeleteAsync(Conference conference);
    }
}
