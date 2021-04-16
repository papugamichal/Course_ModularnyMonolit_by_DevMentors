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
    internal class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly ISpeakerRepository speakerRepository;

        public CreateSubmissionHandler(ISubmissionRepository submissionRepository, 
            ISpeakerRepository speakerRepository)
        {
            this.submissionRepository = submissionRepository;
            this.speakerRepository = speakerRepository;
        }

        public async Task HandleAsync(CreateSubmission command)
        {
            var spekaersId = command.SpeakeresId.Select(x => new AggregateId(x));
            var speakers = await this.speakerRepository.BrowseAsync(spekaersId);

            if (speakers.Count() != spekaersId.Count())
            {
                throw new InvalidSpeakersNumberException(command.Id);
            }

            var submission = Submission.Create(command.Id, command.ConferenceId, command.Title,
                command.Description, command.Level, command.Tags, speakers.ToList());

            await this.submissionRepository.AddAsync(submission);
        }
    }
}
