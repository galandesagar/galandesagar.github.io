using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class InvoiceAutoMailSetting
    {
        public int Id { get; set; }
        public string Reminder { get; set; }
        public int NoOfDay { get; set; }
    }
}