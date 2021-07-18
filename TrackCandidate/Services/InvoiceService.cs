using Priority_Express_Service.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using TrackCandidate.Models;

namespace TrackCandidate.Services
{
    public class InvoiceService
    {
        private readonly SqlServerRepository _sqlServerRepository;

        public InvoiceService()
        {
            _sqlServerRepository = new SqlServerRepository();
        }
        public List<CandidateInvoices> GetCandidateInvoices()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetCandidateInvoices";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<CandidateInvoices>(sqlresult);
            return output;
        }


        public List<VendorCandidatesInvoice> GETCandidateByVendorId(int VendorId, int CandId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorId", VendorId));
            parameter.Add(new SqlParameter("@CandId", CandId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetVendorCandidateInvoiceDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<VendorCandidatesInvoice>(sqlresult);
            return output;
        }

        public bool SaveInvoiceDate(VendorCandidatesInvoice VendorCandidatesInvoice)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorId", VendorCandidatesInvoice.VendorId));
            parameter.Add(new SqlParameter("@CandId", VendorCandidatesInvoice.CandId));
            parameter.Add(new SqlParameter("@InvoiceDate", VendorCandidatesInvoice.InvoiceDate));
            parameter.Add(new SqlParameter("@InvoiceDueDate", VendorCandidatesInvoice.InvoiceDueDate));
            parameter.Add(new SqlParameter("@Status", VendorCandidatesInvoice.Status));
            parameter.Add(new SqlParameter("@MonthId", VendorCandidatesInvoice.MonthId));
            parameter.Add(new SqlParameter("@YearId", VendorCandidatesInvoice.YearId));
            parameter.Add(new SqlParameter("@InvoiceWeekStartDate", VendorCandidatesInvoice.InvoiceWeekStartDate));
            parameter.Add(new SqlParameter("@InvoiceWeekEndDate", VendorCandidatesInvoice.InvoiceWeekEndDate));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SaveInvoiceDate";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            return true;
        }

        public List<CandidateInvoiceLogDetails> GetCandidateInvoiceLogDetails(int VendorId, int CandId, int MonthId, int YearId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorId", VendorId));
            parameter.Add(new SqlParameter("@CandId", CandId));
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            parameter.Add(new SqlParameter("@YearId", YearId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetCandidateInviceLogDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<CandidateInvoiceLogDetails>(sqlresult);
            return output;
        }

        public List<InvoiceReport> GetInvoiceReport(int YearId, int InvoiceType)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@YearId", YearId));
            parameter.Add(new SqlParameter("@InvoiceType", InvoiceType));

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SP_GetInvoiceReport";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<InvoiceReport>(sqlresult);
            return output;
        }

        public bool SendInvoiceMailnotification(int InvoiceId, int TemplateId, string MailBody,string MailSubject)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@InvoiceId", InvoiceId));


            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SP_GetInvoiceVendorMail";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<VendorMail>(sqlresult);
            if (output.Count != 0)
            {
                //if (output[0].InvoiceMailRemainder != 2)
                //{
                SendEmailAlerts(output[0].MailTo, output[0].MailCc, output[0].MailBcc, output[0].InvoiceMailRemainder, output[0].ContactPerson, MailBody, MailSubject);
                updatereminderCount(InvoiceId, output[0].InvoiceMailRemainder, TemplateId, MailBody);
                // }
            }
            return true;
        }
        public static void SendEmailAlerts(string MailTo, string MailCc, string MailBcc, int MailCount, string ContactPerson, string MailBody,string MailSubject)
        {

            StringBuilder SbMsg = new StringBuilder();
            try
            {
                SbMsg.Append("<table cellspacing='2' cellpadding='0' width='600px' border='0'>");
                SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'>Hi " + ContactPerson + ",</td></tr>");

                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>" + MailBody + "</td></tr>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br></td></tr>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br>Sincerely,</td><td align='center'><strong></strong></td>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br></td></tr>");
                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Atul Dubey</td></tr>");
                SbMsg.Append("<tr><td style='text-align: left;font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Software Merchant Inc.</td><td align='center'><strong></strong></td>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>487 Devon Park Drive, Suite 215</td><td align='center'><strong></strong></td>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Wayne PA 19087</td><td align='center'><strong></strong></td>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Tel: <span style='text - decoration:underline; color: blue'>(215) 758-0599</span></td><td align='center'><strong></strong></td>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>e-mail: sm_invoices@softwaremerchant.com</td><td align='center'><strong></strong></td>");
                SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>web: www.softwaremerchant.com</td><td align='center'><strong></strong></td>");

                SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                MailManager oMail = new MailManager();

                oMail.SendEnquiryEmail(MailTo, MailCc, MailBcc, MailSubject, SbMsg.ToString());


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool sendInvoiceDailymail()
        //{
        //    MailManager oMail = new MailManager();

        //    oMail.SendEnquiryEmail("Neeraj_Mathawan@softwareMerchant.com", "atul1502@softwaremerchant.com", "Sagar.galande1119@consoaring.com", "Daily mail notification test", null);

        //    return true;
        //}

        public bool InvoiceDueDateReminderSevenDayBefore(int DayNumber)
        {
            MailManager oMail = new MailManager();

            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@DayNumber", @DayNumber));

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetInvoiceDetailsforSendReminder";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<VendorMail>(sqlresult);
            if (output.Count != 0)
            {
                foreach (var item in output)
                {
                    StringBuilder SbMsg = new StringBuilder();
                    try
                    {
                       
                        SbMsg.Append("<table cellspacing='2' cellpadding='0' width='600px' border='0'>");
                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'>Hi " + item.ContactPerson + ",</td></tr>");

                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>Hope you are having a good week and staying safe during these difficult times we are all experiencing.  I was reaching out as the payment for last month’s invoice is due or will be soon. <br/> Please let us know when you are planning to release the payment.  Thanks and have a great rest of your day and week.</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br>Sincerely,</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br></td></tr>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Atul Dubey</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left;font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Software Merchant Inc.</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>487 Devon Park Drive, Suite 215</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Wayne PA 19087</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Tel: <span style='text - decoration:underline; color: blue'>(215) 758-0599</span></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>e-mail: sm_invoices@softwaremerchant.com</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>web: www.softwaremerchant.com</td><td align='center'><strong></strong></td>");

                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        string Subject = "Payment Reminder";
                        oMail.SendEnquiryEmail(item.MailTo, item.MailCc, item.MailBcc, Subject, SbMsg.ToString());


                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    // updatereminderCount(item.InvoiceId, item.InvoiceMailRemainder,0);

                }
            }
            return true;
        }

        public bool InvoiceDueDateReminderonDay(int DayNumber)
        {
            MailManager oMail = new MailManager();

            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@DayNumber", @DayNumber));

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetInvoiceDetailsforSendReminder";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<VendorMail>(sqlresult);
            if (output.Count != 0)
            {
                foreach (var item in output)
                {

                    StringBuilder SbMsg = new StringBuilder();
                    try
                    {
                        SbMsg.Append("<table cellspacing='2' cellpadding='0' width='600px' border='0'>");
                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'>Hi " + item.ContactPerson + ",</td></tr>");

                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>Just following up our previous e-mail to see if the payment for last month’s invoice has been setup to be released.  According to our records it is now due<br/> so please let us know when the payment will go out this month.  Thanks and look forward to hearing back from you.</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br>Sincerely,</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br></td></tr>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Atul Dubey</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left;font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Software Merchant Inc.</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>487 Devon Park Drive, Suite 215</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Wayne PA 19087</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Tel: <span style='text - decoration:underline; color: blue'>(215) 758-0599</span></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>e-mail: sm_invoices@softwaremerchant.com</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>web: www.softwaremerchant.com</td><td align='center'><strong></strong></td>");

                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                        string Subject = "2nd Payment Reminder";
                        oMail.SendEnquiryEmail(item.MailTo, item.MailCc, item.MailBcc, Subject, SbMsg.ToString());


                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    // updatereminderCount(item.InvoiceId, item.InvoiceMailRemainder,0);

                }
            }
            return true;
        }

        public void updatereminderCount(int InvoiceId, int InvoiceMailRemainder, int TemplateId, string MailBody)
        {
            var Cmd = new SqlCommand();
            var pmr = Cmd.Parameters;
            int count = InvoiceMailRemainder + 1;
            pmr.Add(new SqlParameter("@MailCount", count));
            pmr.Add(new SqlParameter("@InvoiceId", InvoiceId));
            pmr.Add(new SqlParameter("@TemplateId", TemplateId));
            pmr.Add(new SqlParameter("@MailBody", MailBody));

            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "SaveInvoicemailRemainderCount";
            var sqlresults = _sqlServerRepository.ExecuteStoredProcedure(Cmd);
        }
        public List<InvoiceMailDetails> GetInvoiceMailDetails(int InvoiceId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@InvoiceId", InvoiceId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetInvoiceSendMailDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<InvoiceMailDetails>(sqlresult);
            return output;
        }
        public List<MailTemplates> GetSendMailDetails(int SaveTemplateId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@SaveTemplateId", SaveTemplateId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetSendMailDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<MailTemplates>(sqlresult);
            return output;
        }

        public List<InvoiceAutoMailSetting> GetInvoiceAutoMailSetting()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetInvoiceAutoMailSetting";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<InvoiceAutoMailSetting>(sqlresult);
            return output;
        }

        public void SaveInvoiceAutoMailSetting(int Id, string Reminder, int NoOfDays)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@Id", Id));
            parameter.Add(new SqlParameter("@Reminder", Reminder));
            parameter.Add(new SqlParameter("@NoOfDays", NoOfDays));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SaveInvoiceAutoMailSetting";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);

        }
        public void SaveMailTemplate(string TemplateBody,string Subject,string TemplateName)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@TemplateBody", TemplateBody));
            parameter.Add(new SqlParameter("@MailSubject", Subject));
            parameter.Add(new SqlParameter("@TemplateName", TemplateName));

            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SaveNewMailTemplate";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
        }
        public List<MailTemplates> GetMailTemplate(int TemplateId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@TemplateId", TemplateId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetMailTemplate";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<MailTemplates>(sqlresult);
            return output;
        }

    }
}