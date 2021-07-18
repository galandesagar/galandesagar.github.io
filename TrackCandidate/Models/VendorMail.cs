using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class VendorMail
    {
        public int InvoiceId { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public int InvoiceMailRemainder { get; set; }
        public string ContactPerson { get; set; }
    }
}