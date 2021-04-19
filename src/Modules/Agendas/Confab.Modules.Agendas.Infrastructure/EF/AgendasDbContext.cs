﻿using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Infrastructure.EF
{
    internal class AgendasDbContext : DbContext
    {
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public AgendasDbContext(
            DbContextOptions<AgendasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("agendas");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
