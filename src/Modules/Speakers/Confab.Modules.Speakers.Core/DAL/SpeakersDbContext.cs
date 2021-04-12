using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL
{
    internal class SpeakersDbContext : DbContext
    {
        private readonly DbContextOptions<SpeakersDbContext> options;

        public DbSet<Speaker> Speakers { get; set; }

        public SpeakersDbContext(
            DbContextOptions<SpeakersDbContext> options) : base(options)
        {
            this.options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("speakers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
