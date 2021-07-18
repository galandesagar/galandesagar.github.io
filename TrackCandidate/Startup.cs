using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackCandidate.Services;

namespace TrackCandidate
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
       

            GlobalConfiguration.Configuration

                .UseSqlServerStorage("MsSql");
            InvoiceService invoiceService = new InvoiceService();
            TimesheetService timesheetService = new TimesheetService();

            //RecurringJob.AddOrUpdate(() => invoiceService.sendInvoiceDailymail(), Cron.Daily(23,00),TimeZoneInfo.Local);
            //Send mail on invoice due date
            RecurringJob.AddOrUpdate(() => invoiceService.InvoiceDueDateReminderonDay(0), Cron.Daily(23, 00), TimeZoneInfo.Local);
            // Send Mail on Invoice one week before
            RecurringJob.AddOrUpdate(() => invoiceService.InvoiceDueDateReminderSevenDayBefore(7), Cron.Daily(23, 00), TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => timesheetService.SendTimesheetreminderMail(), Cron.Daily(16, 00), TimeZoneInfo.Local);


            app.UseHangfireDashboard();

            app.UseHangfireServer();
        }
        
    }
}