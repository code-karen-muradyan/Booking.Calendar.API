using Autofac;
using Booking.Calendar.API.Application.Queries;
using Booking.Calendar.API.Infrastructure.Idempotency;
using Booking.Calendar.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Calendar.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
       : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new CalendarQueries(QueriesConnectionString))
                .As<ICalendarQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CalendatRepository>()
                .As<ICalendatRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();
        }
    }
}
