using System;
using Confab.Shared.Abstraction.Time;

namespace Confab.Shared.Infrastructure.Time
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
