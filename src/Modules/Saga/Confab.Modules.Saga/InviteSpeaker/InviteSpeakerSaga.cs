﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstraction.Messaging;
using Confab.Shared.Abstraction.Modules;

namespace Confab.Modules.Saga.InviteSpeaker
{
    internal class InviteSpeakerSaga : Saga<InviteSpeakerSaga.SagaData>,
        ISagaStartAction<UserSignedUp>,
        ISagaAction<SpeakerCreated>,
        ISagaAction<UserSignedIn>
    {
        private readonly IModuleClient moduleClient;
        private readonly IMessageBroker messageBroker;

        public override SagaId ResolveId(object message, ISagaContext context)
            => message switch
            {
                UserSignedUp m => m.UserId.ToString(),
                UserSignedIn m => m.UserId.ToString(),
                SpeakerCreated m => m.Id.ToString(),
                _ => base.ResolveId(message, context)
            };

        public InviteSpeakerSaga(IModuleClient moduleClient, IMessageBroker messageBroker)
        {
            this.moduleClient = moduleClient;
            this.messageBroker = messageBroker;
        }

        public Task CompensateAsync(UserSignedUp message, ISagaContext context)
            => Task.CompletedTask;

        public Task CompensateAsync(SpeakerCreated message, ISagaContext context)
            => Task.CompletedTask;

        public Task CompensateAsync(UserSignedIn message, ISagaContext context)
            => Task.CompletedTask;

        public async Task HandleAsync(UserSignedUp message, ISagaContext context)
        {
            var (userId, email) = message;
            if (Data.InvitedSpeakers.TryGetValue(email, out var fullName))
            {
                Data.Email = email;
                Data.FullName = fullName;

                await moduleClient.SendAsync("speakers/create", new
                {
                    Id = userId,
                    Email = email,
                    FullName = fullName,
                    Bio = "Lorem Ipsum"
                });

                return;
            }

            await CompleteAsync();
        }

        public Task HandleAsync(SpeakerCreated message, ISagaContext context)
        {
            Data.SpeakerCreated = true;
            return Task.CompletedTask;
        }

        public async Task HandleAsync(UserSignedIn message, ISagaContext context)
        {
            if (Data.SpeakerCreated)
            {
                await messageBroker.PublishAsync(new SendWelcomeMessage(Data.Email, Data.FullName));
                await CompleteAsync();
            }
        }

        internal class SagaData
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public bool SpeakerCreated { get; set; }


            public readonly Dictionary<string, string> InvitedSpeakers = new()
            {
                ["testspeaker1@confab.io"] = "John Smith",
                ["testspeaker2@confab.io"] = "Mark Sim"
            };
        }
    }
}
