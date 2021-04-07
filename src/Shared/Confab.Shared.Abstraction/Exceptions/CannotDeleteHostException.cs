using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Exceptions
{
    public class CannotDeleteHostException : ConfabException
    {
        public Guid Id { get; }

        public CannotDeleteHostException(Guid id) : base($"Host with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }
    }
}
