using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.Submisions.Repositories;
using Confab.Shared.Abstraction.Commands;
using Confab.Shared.Abstraction.Kernel;
using Confab.Shared.Abstraction.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly IDomainEventHandlerDispatcher domainEventHandlerDispatcher;
        private readonly IEventMapper eventMapper;
        private readonly IMessageBroker messageBroker;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository, 
            IDomainEventHandlerDispatcher domainEventHandlerDispatcher,
            IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            this.submissionRepository = submissionRepository;
            this.domainEventHandlerDispatcher = domainEventHandlerDispatcher;
            this.eventMapper = eventMapper;
            this.messageBroker = messageBroker;
        }

        public async Task HandleAsync(ApproveSubmission command)
        {
            var submission = await this.submissionRepository.GetAsync(command.SubmissionId);
            if (submission is null)
            {
                throw new SubmissionNotFoundException(command.SubmissionId);
            }

            submission.Approve();

            await this.submissionRepository.UpdateAsync(submission);
            await this.domainEventHandlerDispatcher.DispatchAsync(submission.Event.ToArray());

            var events = eventMapper.MapAll(submission.Event);
            await messageBroker.PublishAsync(events.ToArray());
        }
    }
}
