using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class EditCandidateDTO
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public int vendorId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StatusId { get; set; }
        public string Email { get; set; }
    }
}