using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakersRepository : ISpeakerRepository
    {
        private readonly SpeakersDbContext speakerDbContext;

        public SpeakersRepository(SpeakersDbContext speakerDbContext)
        {
            this.speakerDbContext = speakerDbContext ?? throw new ArgumentNullException(nameof(speakerDbContext));
        }

        public async Task AddAsync(Speaker speaker)
        {
            await this.speakerDbContext.AddAsync(speaker);
            await this.speakerDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Speaker>> BrowseAsync()
        {
            return this.speakerDbContext.Speakers.ToList();
        }

        public async Task DeleteAsync(Speaker speaker)
        {
            this.speakerDbContext.Remove(speaker);
            await this.speakerDbContext.SaveChangesAsync();
        }

        public Task<Speaker> GetAsync(Guid id)
        {
            return this.speakerDbContext.Speakers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await this.speakerDbContext.Speakers.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Speaker speaker)
        {
            this.speakerDbContext.Speakers.Update(speaker);
            await this.speakerDbContext.SaveChangesAsync();
        }
    }
}
