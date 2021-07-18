using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class AddTimesheetDTO
    {
        public int CandidateId { get; set; }
        public int TimeCardId { get; set; }
        public decimal ClientHolidayHours { get; set; }
        public string ClientHolidayId { get; set; }
        public string ClientHolidayComment { get; set; }
        public decimal Paid { get; set; }
        public decimal unpaid { get; set; }
        public string PTOComment { get; set; }
        public int WorkHrs { get; set; }
        public int ClientHrs { get; set; }
        public int MaxHrs { get; set; }
        public string file { get; set; }
    }
}