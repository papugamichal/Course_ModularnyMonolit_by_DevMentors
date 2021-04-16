using Confab.Shared.Abstraction.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External
{
    public class SpeakerCreated : IEvent
    {
        public SpeakerCreated(Guid id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public Guid Id { get; }
        public string FullName { get; }
    }
}
