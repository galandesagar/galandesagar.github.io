using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class CandidatePTODetails
    {
        public decimal TotalPTO { get; set; }
        public decimal EligiblePTO { get; set; }
        public decimal AvailablePTO { get; set; }
        public decimal PTOTaken { get; set; }
    }
}