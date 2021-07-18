using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class Candidate
    {
        public int CanId { get; set; }
        public string Name { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TimeCardId { get; set; }
        public string FileName { get; set; }
        public string Email { get; set; }
    }
}