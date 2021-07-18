using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class InvoiceReport
    {
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string MonthId { get; set; }
        public int YearId { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDueDate { get; set; }
        public string InvoiceWeekStartDate { get; set; }
        public string InvoiceWeekEndDate { get; set; }
    }
}