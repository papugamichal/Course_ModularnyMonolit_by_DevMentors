using Confab.Shared.Abstraction.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Events
{
    public class ConferenceCreated : IEvent
    {
        public ConferenceCreated(Guid conferenceId, string name, int? participantsList, DateTime from, DateTime to)
        {
            ConferenceId = conferenceId;
            Name = name;
            ParticipantsList = participantsList;
            From = from;
            To = to;
        }

        public Guid ConferenceId { get; }
        public string Name { get; }
        public int? ParticipantsList { get; }
        public DateTime From { get; }
        public DateTime To { get; }
    }
}
