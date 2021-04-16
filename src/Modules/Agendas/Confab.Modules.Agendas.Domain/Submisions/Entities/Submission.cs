using Confab.Modules.Agendas.Domain.Submisions.Consts;
using Confab.Modules.Agendas.Domain.Submisions.Events;
using Confab.Modules.Agendas.Domain.Submisions.Exceptions;
using Confab.Shared.Abstraction.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Domain.Submisions.Entities
{
    public sealed class Submission : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Title{ get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public string Status { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public IEnumerable<Speaker> Speakers => speakers;

        private ICollection<Speaker> speakers;

        //ctor for recreating entity from db. Assume that data in DB are correct
        public Submission(AggregateId id, ConferenceId conferenceId,
            string title, string description, int level, string status,
            IEnumerable<string> tags, ICollection<Speaker> speakers, int version = 0)
            : this(id, conferenceId)
        {
            Title = title;
            Description = description;
            Level = level;
            Status = status;
            Tags = tags;
            this.speakers = speakers;
            Version = version;
        }

        public Submission(AggregateId id, ConferenceId conferenceId)
            => (Id, ConferenceId) = (id, conferenceId);

        public static Submission Create(AggregateId id, ConferenceId conferenceId,
            string title, string description, int level,
            IEnumerable<string> tags, ICollection<Speaker> speakers)
        {
            var submission = new Submission(id, conferenceId);
            submission.ChangeTitle(title);
            submission.ChangeDescription(description);
            submission.ChangeLevel(level);
            submission.Status = SubmissionStatus.Pending;
            submission.Tags = tags;
            submission.ChangeSpeakers(speakers);
            submission.ClearEvents();
            submission.Version = 0;

            submission.AddEvent(new SubmissionAdded(submission));
            return submission;
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new EmptySubmissionTitleException(Id);
            }

            Title = title;
            IncrementVersion();
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new EmptySubmissionDescriptionException(Id);
            }

            Description = description;
            IncrementVersion();
        }

        public void ChangeLevel(int level)
        {
            if (IsNotInRange())
            {
                throw new InvalidSubmissionLevelException(Id);
            }

            Level = level;
            IncrementVersion(); 
            
            bool IsNotInRange() => level < 1 || level > 6;
        }

        public void ChangeSpeakers(IEnumerable<Speaker> speakers)
        {
            if (speakers is null || !speakers.Any())
                throw new MissingSubmissionSpeakersException(Id);

            this.speakers = speakers.ToList();
            IncrementVersion();
        }

        public void Approve()
            => ChangeStatus(SubmissionStatus.Approved, SubmissionStatus.Rejected);

        public void Reject()
            => ChangeStatus(SubmissionStatus.Rejected, SubmissionStatus.Approved);

        private void ChangeStatus(string status, string invalidStatus)
        {
            if (this.Status == invalidStatus)
            {
                throw new InvalidSubmissionStatusException(Id, status, invalidStatus);
            }

            Status = status;
            AddEvent(new SubmissionStatusChanged(this, status));
        }
    }
}
