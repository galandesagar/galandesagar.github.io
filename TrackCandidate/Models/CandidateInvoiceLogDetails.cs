using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class CandidateInvoiceLogDetails
    {
        public int InvoiceId { get; set; }
        public string CandName { get; set; }
        public string VendorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
    }
}