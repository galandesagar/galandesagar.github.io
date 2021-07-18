using Priority_Express_Service.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TrackCandidate.Models;

namespace TrackCandidate.Services
{
    public class TimesheetService
    {
        private readonly SqlServerRepository _sqlServerRepository;
        public TimesheetService()
        {
            _sqlServerRepository = new SqlServerRepository();

        }

        public int AddTimeSheet(AddTimesheetDTO addTimesheetDTO)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandidateId", addTimesheetDTO.CandidateId));
            parameter.Add(new SqlParameter("@TimeCardId", addTimesheetDTO.TimeCardId));
            parameter.Add(new SqlParameter("@Paid", addTimesheetDTO.Paid));
            parameter.Add(new SqlParameter("@Unpaid", addTimesheetDTO.unpaid));
            parameter.Add(new SqlParameter("@PTOComment", addTimesheetDTO.PTOComment));
            parameter.Add(new SqlParameter("@WorkHrs", addTimesheetDTO.WorkHrs));
            parameter.Add(new SqlParameter("@ClientHrs", addTimesheetDTO.ClientHrs));
            parameter.Add(new SqlParameter("@MaxHrs", addTimesheetDTO.MaxHrs));
            parameter.Add(new SqlParameter("@FileName", addTimesheetDTO.file));
            parameter.Add(new SqlParameter("@ClientHolidayHours", addTimesheetDTO.ClientHolidayHours));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "InsertTimesheet";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32(parameter["@rowCount"].Value);
            string[] values = addTimesheetDTO.ClientHolidayId.Split(',').Select(sValue => sValue.Trim()).ToArray();
            string[] Comment = addTimesheetDTO.ClientHolidayComment.Split(',').Select(sValue => sValue.Trim()).ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                var Command1 = new SqlCommand();
                var parameter1 = Command1.Parameters;
                parameter1.Add(new SqlParameter("@CandidateId", addTimesheetDTO.CandidateId));
                parameter1.Add(new SqlParameter("@TimeCardId", addTimesheetDTO.TimeCardId));
                parameter1.Add(new SqlParameter("@ClientHolidayId", values[i]));
                Command1.CommandType = CommandType.StoredProcedure;
                Command1.CommandText = "insertClientHolidayDetails";
                var sqlresult1 = _sqlServerRepository.ExecuteNonQuery(Command1);
            }
            return 1;
        }

        public void SaveTimesheetAutoMailReminderSetting(int Id, string Reminder, int NoOfDays)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@Id", Id));
            parameter.Add(new SqlParameter("@Reminder", Reminder));
            parameter.Add(new SqlParameter("@NoOfDays", NoOfDays));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "SaveTimesheetAutoMailReminderSetting";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);

        }
        public List<TimesheetAutoMailRemindercs> GetTimesheetAutoMailReminderSetting()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetTimesheetAutoMailReminderSetting";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<TimesheetAutoMailRemindercs>(sqlresult);
            return output;
        }

        public bool SendTimesheetreminderMail()
        {
            MailManager oMail = new MailManager();

            var Command = new SqlCommand();
          
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetTimesheetReminderDetail";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<CandidateMail>(sqlresult);
            if (output.Count != 0)
            {
                foreach (var item in output)
                {

                    StringBuilder SbMsg = new StringBuilder();
                    try
                    {
                        SbMsg.Append("<table cellspacing='2' cellpadding='0' width='600px' border='0'>");
                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'>Hi " + item.Name + ",</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                        SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>Greetings from Software Merchant Inc!</td></tr>");
                        if (item.Reminder == 1)
                        {
                            SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>Kindly share your time card for Current week, so we can update your work hours.</td></tr>");
                        }
                        else
                        {
                            SbMsg.Append("<tr><td style='width: 100%; text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px' colspan='3'><br>Kindly share your time card for Last week, so we can update your work hours.</td></tr>");
                        }
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br></td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br><br>Sincerely,</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'><br></td></tr>");
                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Atul Dubey | Mobility Consultant</td></tr>");
                        SbMsg.Append("<tr><td style='text-align: left;font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Software Merchant Inc.</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>487 Devon Park Drive, Suite 215</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Wayne PA 19087</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>Tel: <span style='text - decoration:underline; color: blue'>(215) 758-0599</span></td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>e-mail: atul1502@softwaremerchant.com</td><td align='center'><strong></strong></td>");
                        SbMsg.Append("<tr><td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'>web: www.softwaremerchant.com</td><td align='center'><strong></strong></td>");

                        SbMsg.Append("<td style='text-align: left; font-family: Calibri, Candara, Segoe, Segoe UI, Optima, Arial, sans-serif;font-size: 16.5px'></td></tr>");

                        string Subject = "Time Card Reminder";
                        oMail.SendTimesheetreminderEmail( item.Email, "atul1502@softwaremerchant.com", Subject, SbMsg.ToString());


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

        public void AddWeekDates(int MonthId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "InsertWeekDatestotblTimesheet";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);

        }
    }
}