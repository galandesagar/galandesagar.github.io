using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TrackCandidate.Models
{
    class MailManager
    {
        #region Private for email
        private string FromAddress { get; set; }
        private string TimesheetFromAddress { get; set; }
        private string EmailHost { get; set; }
        private string Pwd { get; set; }
        private string UserID { get; set; }
        private string SMTPPort { get; set; }
        private Boolean bEnableSSL { get; set; }


        #endregion


        #region Default Functions for Mailing
        public MailManager()
        {
            FromAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"];
            TimesheetFromAddress = System.Configuration.ConfigurationManager.AppSettings["TimesheetFromAddress"];
            UserID = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
            SMTPPort = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort");
            Pwd = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            EmailHost = System.Configuration.ConfigurationManager.AppSettings.Get("SmtpClient");

            if (System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL").ToUpper() == "YES")
            {
                bEnableSSL = true;
            }
            else
            {
                bEnableSSL = false;
            }
        }


        private bool SendFinalMail(MailMessage MailInfo)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = EmailHost;
                smtp.Port = Convert.ToInt16(SMTPPort);
                smtp.Credentials = new System.Net.NetworkCredential(UserID, Pwd);
                smtp.EnableSsl = bEnableSSL;
                //smtp.Send(MailInfo);

                System.Threading.Thread emailThread;
                emailThread = new System.Threading.Thread(delegate ()
                {
                    smtp.Send(MailInfo);
                });

                emailThread.IsBackground = true;
                emailThread.Start();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        #endregion


        public bool SendEnquiryEmail(string ToEmail,string CCMail,string BccMail,string Subject, string htmlBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromAddress);
                mail.To.Add(ToEmail);
                mail.CC.Add(CCMail);
                mail.Bcc.Add(BccMail);
                mail.Subject = Subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                return this.SendFinalMail(mail);
            }
            catch
            {
                return false;
            }
        }

        public bool SendTimesheetreminderEmail(string ToEmail, string CCMail,  string Subject, string htmlBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(TimesheetFromAddress);
                mail.To.Add(ToEmail);
                mail.CC.Add(CCMail);
                mail.Subject = Subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                return this.SendFinalMail(mail);
            }
            catch
            {
                return false;
            }
        }

    }
}