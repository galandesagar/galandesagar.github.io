using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class LeavesCalculation
    {
        public int MonthId { get; set; }
        public string MonthName { get; set; }

        public decimal Calculation { get; set; }
    }
}