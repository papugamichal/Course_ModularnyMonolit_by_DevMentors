﻿using Confab.Shared.Abstraction.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public class ApproveSubmission : ICommand
    {
        public ApproveSubmission(Guid submissionId)
        {
            SubmissionId = submissionId;
        }

        public Guid SubmissionId { get; }
    }
}
