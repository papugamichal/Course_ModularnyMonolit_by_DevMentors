using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Shared.Abstraction.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Infrastructure.Queries.Handlers
{
    internal sealed class GetSubmissionsHandler : IQueryHandler<GetSubmission, SubmissionDto>
    {
        private readonly AgendasDbContext agendasDbContext;

        public GetSubmissionsHandler(AgendasDbContext agendasDbContext)
        {
            this.agendasDbContext = agendasDbContext;
        }

        public async Task<SubmissionDto> HandleAsync(GetSubmission query)
        {
            return await this.agendasDbContext.Submissions
                .AsNoTracking()
                .Where(x => x.Id.Equals(query.Id))
                .Include(x => x.Speakers)
                .Select(x => new SubmissionDto
                {
                    Id = x.Id,
                    ConferenceId = x.ConferenceId,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    Tags = x.Tags,
                    Speakers = x.Speakers.Select(s => new SpeakerDto()
                    {
                        Id = s.Id,
                        FullName = s.FullName
                    })
                })
                .SingleOrDefaultAsync();
        }
    }
}
