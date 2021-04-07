using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Exceptions
{
    public class ConferenceNotFoundException : ConfabException
    {
        public Guid Id { get; }
     
        public ConferenceNotFoundException(Guid id) : base($"Conference with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}
