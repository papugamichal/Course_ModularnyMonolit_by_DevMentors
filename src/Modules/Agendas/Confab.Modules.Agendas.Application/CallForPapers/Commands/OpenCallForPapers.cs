﻿using System;
using Confab.Shared.Abstraction.Commands;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands
{
    public record OpenCallForPapers(Guid ConferenceId) : ICommand;
}
