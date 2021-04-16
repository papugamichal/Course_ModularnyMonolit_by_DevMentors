using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Queries
{
    // Marker
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IQuery
    {

    }
}
