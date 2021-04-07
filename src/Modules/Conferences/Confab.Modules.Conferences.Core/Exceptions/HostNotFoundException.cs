using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Shared.Abstraction.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    public class HostNotFoundException : ConfabException
    {
        public Guid Id { get; }
     
        public HostNotFoundException(Guid id) : base($"Host with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}
