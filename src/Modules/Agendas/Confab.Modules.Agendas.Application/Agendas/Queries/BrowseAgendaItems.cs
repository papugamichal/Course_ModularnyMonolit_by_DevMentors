﻿using System;
using System.Collections.Generic;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Shared.Abstraction.Queries;

namespace Confab.Modules.Agendas.Application.Agendas.Queries
{
    public class BrowseAgendaItems : IQuery<IEnumerable<AgendaItemDto>>
    {
        public Guid ConferenceId { get; set; }
    }
}