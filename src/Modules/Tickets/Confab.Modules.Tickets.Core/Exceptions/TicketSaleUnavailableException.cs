using Confab.Shared.Abstraction.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Tickets.Core.Exceptions
{
    public class TicketSaleUnavailableException : ConfabException
    {
        public Guid ConferenceId { get; }

        public TicketSaleUnavailableException(Guid conferenceId)
            : base("Ticket sale for the conference is unavailable.")

        {
            ConferenceId = conferenceId;
        }
    }
}
