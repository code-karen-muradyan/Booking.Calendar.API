using Booking.Calendar.API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.EntityConfigurations
{
    public class GoogleEventEntityTypeConfiguration : IEntityTypeConfiguration<IDSpecification>
    {
        public void Configure(EntityTypeBuilder<IDSpecification> builder)
        {
            builder.HasOne<Apponintment>(ad => ad.Apponintment)
                   .WithOne(s => s.SpecifiedID);
        }
    }
}
