using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Confab.Modules.Speakers.Core.DAL.Configurations
{
    internal class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
        }
    }
}
