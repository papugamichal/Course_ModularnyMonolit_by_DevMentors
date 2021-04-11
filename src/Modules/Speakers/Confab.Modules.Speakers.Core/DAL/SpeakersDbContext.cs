﻿using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
