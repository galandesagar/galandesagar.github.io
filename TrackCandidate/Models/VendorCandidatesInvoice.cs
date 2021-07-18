using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class VendorCandidatesInvoice
    {
        public int CandId  { get; set; }
        public string CandName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDueDate { get; set; }
        public int Status { get; set; }
        public string InvoiceWeekStartDate { get; set; }
        public string InvoiceWeekEndDate { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
    }
}