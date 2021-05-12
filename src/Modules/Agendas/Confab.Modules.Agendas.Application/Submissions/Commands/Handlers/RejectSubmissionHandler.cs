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
    internal class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly IDomainEventHandlerDispatcher domainEventHandlerDispatcher;
        private readonly IEventMapper eventMapper;
        private readonly IMessageBroker messageBroker;

        public RejectSubmissionHandler(
            ISubmissionRepository submissionRepository, 
            IDomainEventHandlerDispatcher domainEventHandlerDispatcher,
            IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            this.submissionRepository = submissionRepository;
            this.domainEventHandlerDispatcher = domainEventHandlerDispatcher;
            this.eventMapper = eventMapper;
            this.messageBroker = messageBroker;
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
            await this.domainEventHandlerDispatcher.DispatchAsync(submission.Event.ToArray());

            var events = eventMapper.MapAll(submission.Event);
            await messageBroker.PublishAsync(events.ToArray());
        }
    }
}
