using Confab.Shared.Abstraction.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public class CreateSubmission : ICommand
    {
        public CreateSubmission(Guid conferenceId, string Title, string Description, int level, 
            IEnumerable<string> tags, IEnumerable<Guid> speakeresId)
        {
            Id = Guid.NewGuid();
            ConferenceId = conferenceId;
            this.Title = Title;
            this.Description = Description;
            Level = level;
            Tags = tags;
            SpeakeresId = speakeresId;
        }

        public Guid Id { get; }
        public Guid ConferenceId { get; }
        public string Title { get; }
        public string Description { get; }
        public int Level { get; }
        public IEnumerable<string> Tags { get; }
        public IEnumerable<Guid> SpeakeresId { get; }
    }
}
