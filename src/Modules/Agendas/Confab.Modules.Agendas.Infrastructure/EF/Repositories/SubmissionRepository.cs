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
    internal sealed class SubmissionRepository : ISubmissionRepository
    {
        private readonly AgendasDbContext dbContext;
        private readonly DbSet<Submission> submissions;

        public SubmissionRepository(AgendasDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.submissions = this.dbContext.Submissions;
        }

        public async Task<Submission> GetAsync(AggregateId id)
            => await submissions.Include(x => x.Speakers).SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Submission submision)
        {
            await this.submissions.AddAsync(submision);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Submission submision)
        {
            this.dbContext.Update(submision);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
