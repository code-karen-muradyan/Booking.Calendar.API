using Booking.Calendar.API.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.EntityConfigurations
{
    public class CalendarEntityTypeConfiguration:IEntityTypeConfiguration<Apponintment>
    {
        public void Configure(EntityTypeBuilder<Apponintment> calendarConfiguration)
        {
            calendarConfiguration.ToTable("apponintment");

            calendarConfiguration.HasKey(o => o.Id);

            calendarConfiguration.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("appointmentseq");

            calendarConfiguration.Property<DateTime>("StartDate").IsRequired();
            calendarConfiguration.Property<string>("From").IsRequired();
            calendarConfiguration.Property<string>("To").IsRequired();
            calendarConfiguration.Property<string>("Categoria").IsRequired(false);
            calendarConfiguration.Property<string>("ClassEvent").IsRequired();
            calendarConfiguration.Property<string>("Title").IsRequired();
            calendarConfiguration.Property<string>("Description").IsRequired(false);
          
        }
    }
}
