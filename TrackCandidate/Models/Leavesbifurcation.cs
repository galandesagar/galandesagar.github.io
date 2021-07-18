using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class Leavesbifurcation
    {
        public int Id { get; set; }
        public string ClientHolidayComment { get; set; }
        public string PTOType  { get; set; }
        public string PTOComment  { get; set; }
        public decimal ClientHolidayHours { get; set; }
        public decimal Hours { get; set; }
    }
}