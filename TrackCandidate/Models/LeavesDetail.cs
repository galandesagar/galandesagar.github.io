using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class LeavesDetail
    {
        public int CandId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public decimal TotalPTO { get; set; }
        public decimal PTOUpToCurrentMonth { get; set; }
        public decimal Leavesavailable { get; set; }
        public decimal LeavesTaken { get; set; }
        public decimal paid { get; set; }
        public decimal unpaid { get; set; }
        public decimal clientPTO { get; set; }
    }
}