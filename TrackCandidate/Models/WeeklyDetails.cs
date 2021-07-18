using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class WeeklyDetails
    {
        public int CandId { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string WeekStartDate { get; set; }
        public string WeekEndDate { get; set; }
        public decimal PTOTaken { get; set; }
        public decimal ClientHoliday { get; set; }
        public int TotalWorkingHours { get; set; }
        public string FileName { get; set; }
    }
}