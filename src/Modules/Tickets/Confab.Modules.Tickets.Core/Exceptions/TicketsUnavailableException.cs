using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    public class TicketsUnavailableException : ConfabException
    {
        public Guid ConferenceId { get; }

        public TicketsUnavailableException(Guid conferenceId)
            : base("There are no available tickets for the conference.")

        {
            ConferenceId = conferenceId;
        }
    }
}
