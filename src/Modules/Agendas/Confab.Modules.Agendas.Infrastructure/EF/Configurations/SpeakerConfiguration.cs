using Confab.Modules.Agendas.Domain.Submisions.Entities;
using Confab.Shared.Abstraction.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    internal class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Value, x => new AggregateId(x));

            builder.Property(x => x.Version).IsConcurrencyToken();
        }
    }
}
