using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackCandidate.Models
{
    public class MailTemplates
    {
        public int TemplateId { get; set; }
        public string TemplateBody { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateName { get; set; }
    }
}