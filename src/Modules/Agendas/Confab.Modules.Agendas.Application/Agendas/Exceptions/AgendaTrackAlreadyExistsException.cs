using System;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions
{
    public class AgendaTrackAlreadyExistsException : ConfabException
    {
        public AgendaTrackAlreadyExistsException(Guid agendaTrackId) 
            : base($"Agenda track with ID: '{agendaTrackId} already exists.'")
        {
        }
    }
}