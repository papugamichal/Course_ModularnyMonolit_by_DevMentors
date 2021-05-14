using System;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class AgendaItemNotFoundException : ConfabException
    {
        public Guid AgendaItemId { get; }

        public AgendaItemNotFoundException(Guid agendaItemId) 
            : base($"AgendaItem with ID: '{agendaItemId}' was not found.")
        {
            AgendaItemId = agendaItemId;
        }
    }
}