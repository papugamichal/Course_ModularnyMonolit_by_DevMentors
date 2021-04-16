using Confab.Modules.Agendas.Application.Exceptions;
using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Shared.Abstraction.Commands;
using Confab.Shared.Abstraction.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;

        public RejectSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
        }

        public async Task HandleAsync(RejectSubmission command)
        {
            var submission = await this.submissionRepository.GetAsync(command.SubmissionId);
            if (submission is null)
            {
                throw new SubmissionNotFoundException(command.SubmissionId);
            }

            submission.Reject();
            await this.submissionRepository.UpdateAsync(submission);
        }
    }
}
