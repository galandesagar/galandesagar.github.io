using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class InvoiceMailDetails
    {
        public int SaveTemplateId { get; set; }
        public string InvoiceDate { get; set; }
        public string SendMailDate { get; set; }
        public int TemplateId { get; set; }
    }
}