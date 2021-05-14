using System;
using System.Collections.Generic;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Shared.Abstraction.Queries;

namespace Confab.Modules.Attendances.Application.Queries
{
    public class BrowseAttendances : IQuery<IReadOnlyList<AttendanceDto>>
    {
        public Guid UserId { get; set; }
        public Guid ConferenceId { get; set; }
    }
}