using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Shared.Abstraction.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class SpeakerRepository : ISpeakerRepository
    {
        private readonly AgendasDbContext dbContext;
        private readonly DbSet<Speaker> speakers;

        public SpeakerRepository(AgendasDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.speakers = this.dbContext.Speakers;
        }

        public async Task<bool> ExistsAsync(AggregateId id)
            => await this.speakers.AnyAsync(x => x.Id.Equals(id));

        public async Task<IEnumerable<Speaker>> BrowseAsync(IEnumerable<AggregateId> ids)
            => await this.speakers.Where(x => ids.Contains(x.Id)).ToListAsync();

        public async Task AddAsync(Speaker speaker)
        {
            await this.speakers.AddAsync(speaker);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
