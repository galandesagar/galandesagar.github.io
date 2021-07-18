using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class CandidateInvoices
    {
        public int CandId { get; set; }
        public string CandName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int PaymentTerm { get; set; }
        public int InvoiceType { get; set; }
        public int InvoiceAvailabel { get; set; }
    }
}