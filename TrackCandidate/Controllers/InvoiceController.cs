using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrackCandidate.Models;
using TrackCandidate.Services;

namespace TrackCandidate.Controllers
{
    public class InvoiceController : ApiController
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController()
        {
            _invoiceService = new InvoiceService();
        }

        [HttpGet]
        [Route("api/Invoice/getCandidateInvoices")]
        public IEnumerable<CandidateInvoices> Get()
        {
            return _invoiceService.GetCandidateInvoices();
        }

        [HttpGet]
        [Route("api/Invoice/GETCandidateByVendorId")]
        public IEnumerable<VendorCandidatesInvoice> GETCandidateByVendorId(int VendorId,int CandId)
        {
            return _invoiceService.GETCandidateByVendorId(VendorId, CandId);
        }


        [HttpPost]
        [Route("api/Invoice/SaveInvoiceDate")]
        public HttpResponseMessage SaveInvoiceDate(VendorCandidatesInvoice VendorCandidatesInvoice)
        {
            var result = _invoiceService.SaveInvoiceDate(VendorCandidatesInvoice);
            if (result != true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = "success" });
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate check vendor name");
            }
        }

        [HttpGet]
        [Route("api/Invoice/GetCandidateInvoiceLogDetails")]
        public List<CandidateInvoiceLogDetails> GetCandidateInvoiceLog(int VendorId, int CandId,int MonthId,int YearId)
        {
            return _invoiceService.GetCandidateInvoiceLogDetails(VendorId, CandId, MonthId,YearId);
        }

        [HttpGet]
        [Route("api/Invoice/GetInvoiceReport")]
        public IEnumerable<InvoiceReport> GetInvoiceReport( int YearId,int InvoiceType)
        {
            return _invoiceService.GetInvoiceReport(YearId,InvoiceType);
        }

        [HttpGet]
        [Route("api/Invoice/sendInvoicemailnotification")]
        public bool sendInvoicemailnotification(int InvoiceId,int TemplateId,string MailBody,string MailSubject)
        {
            return _invoiceService.SendInvoiceMailnotification(InvoiceId, TemplateId, MailBody, MailSubject);
        }
        //[HttpGet]
        //[Route("api/Invoice/sendInvoiceDailymail")]
        //public bool sendInvoiceDailymail()
        //{
        //    return _invoiceService.sendInvoiceDailymail();
        //}

        [HttpGet]
        [Route("api/Invoice/GetInvoiceMailDetails")]
        public IEnumerable<InvoiceMailDetails> GetInvoiceMailDetails(int InvoiceId)
        {
            return _invoiceService.GetInvoiceMailDetails(InvoiceId);
        }

        [HttpGet]
        [Route("api/Invoice/GetSendMailDetails")]
        public IEnumerable<MailTemplates> GetSendMailDetails(int SaveTemplateId)
        {
            var result =   _invoiceService.GetSendMailDetails(SaveTemplateId);
            return result;
        }

        [HttpGet]
        [Route("api/Invoice/getInvoiceAutoMailSetting")]
        public IEnumerable<InvoiceAutoMailSetting> getInvoiceAutoMailSetting()
        {
            var Result = _invoiceService.GetInvoiceAutoMailSetting();
            return Result;
        }
        [HttpPost]
        [Route("api/Invoice/SaveInvoiceAutoMailSetting")]
        public void SaveInvoiceAutoMailSetting(int Id,string Reminder,int NoOfDays)
        {
             _invoiceService.SaveInvoiceAutoMailSetting(Id,Reminder,NoOfDays);
        }

        [HttpPost]
        [Route("api/Invoice/SaveMailTemplate")]
        public void SaveMailTemplate(string TemplateBody,string Subject,string TemplateName)
        {
            _invoiceService.SaveMailTemplate( TemplateBody,Subject, TemplateName);
        }

        [HttpGet]
        [Route("api/Invoice/GetMailTemplate")]
        public IEnumerable<MailTemplates> GetMailTemplate()
        {
            var Result = _invoiceService.GetMailTemplate(0);
            return Result;
        }

        [HttpGet]
        [Route("api/Invoice/GetTemplate")]
        public IEnumerable<MailTemplates> GetTemplate(int TemplateId)
        {
            var Result = _invoiceService.GetMailTemplate(TemplateId);
            return Result;
        }
    }
}
