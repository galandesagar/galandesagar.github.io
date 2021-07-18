using Priority_Express_Service.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using TrackCandidate.Models;

namespace TrackCandidate.Services
{
    public class CandidateService
    {
        private readonly SqlServerRepository _sqlServerRepository;
        public CandidateService()
        {
            _sqlServerRepository = new SqlServerRepository();
        }

        public int AddCandidate(AddCandidateDTO addCandidateDTO)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@Name", addCandidateDTO.Name));
            parameter.Add(new SqlParameter("@vendorId", addCandidateDTO.vendorId));
            parameter.Add(new SqlParameter("@StartDate", addCandidateDTO.StartDate));
            parameter.Add(new SqlParameter("@EndDate", addCandidateDTO.EndDate));
            parameter.Add(new SqlParameter("@Email", addCandidateDTO.Email));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "AddCandidate";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32( parameter["@rowCount"].Value);
            return result;
        }
        public List<Candidate> GetCandidate(int MonthId,int YearId,int StatusId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            parameter.Add(new SqlParameter("@YearId", YearId));
            parameter.Add(new SqlParameter("@StatusId", StatusId));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetCandidate";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<Candidate>(sqlresult);
            var result = parameter["@rowCount"].Value.ToString();
            return output;
        }

        public int DeleteCandidate(int CandidateId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandidateId", CandidateId));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "DeleteCandidate";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32(parameter["@rowCount"].Value);
            return result;
        }

        public int EditCandidate(EditCandidateDTO editCandidateDTO)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandidateId", editCandidateDTO.CandidateId));
            parameter.Add(new SqlParameter("@Name", editCandidateDTO.Name));
            parameter.Add(new SqlParameter("@vendorId", editCandidateDTO.vendorId));
            parameter.Add(new SqlParameter("@EMail", editCandidateDTO.Email));
            parameter.Add(new SqlParameter("@StatusId", editCandidateDTO.StatusId));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "EditCandidate";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32(parameter["@rowCount"].Value);
            return result;
        }

        public List<Candidate> GetcandidateDetails()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetCandidateDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<Candidate>(sqlresult);
            var result = parameter["@rowCount"].Value.ToString();
            return output;
        }

        public List<ClientHoliday> getClientHolidayList()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetClientHolidayList";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<ClientHoliday>(sqlresult);
            return output;
        }

        public void addClientHoliday(string HolidayName, string Date)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@HolidayName", HolidayName));
            parameter.Add(new SqlParameter("@Date", Date));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "insertclientHoliday";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            
        }

        public List<CandidatePTODetails> candidatePTODetails(int MonthId, int YearId, int CandId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            parameter.Add(new SqlParameter("@Year", YearId));
            parameter.Add(new SqlParameter("@CandId", CandId));
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetPTODetailsMonthWise";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<CandidatePTODetails>(sqlresult);
            return output;
        }
        public bool SendPTOMail(string CandName, int PTOAvbl, int PTOTaken, string comment)
        {
            var result = getHtml( CandName,  PTOAvbl,  PTOTaken,  comment);
            var response = Email(result);
            return true;
        }

        
        public static string getHtml(string CandName, int PTOAvbl, int PTOTaken, string comment)
        {
            try
            {
                string messageBody = "<h3>Candidate PTO Report: </h3>";
                messageBody += "<span> Candidate Name:- " + CandName + "</span><br/>";
                messageBody += "<span> PTO Available :- " + PTOAvbl + " Hrs</span><br/>";
                messageBody += "<span> PTO Taken :- " + PTOTaken + " Hrs</span><br/>";
                messageBody += "<span>Description :- " + comment + "</span><br/><br/>";
                messageBody += "<span>Best Regards</span><br/>";
                messageBody += "<span>Software merchant inc.</span><br/>";

                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string Email(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("Sagar.Galande@consoaring.com");
                message.To.Add(new MailAddress("neeraj_mathawan@softwaremerchant.com"));
                message.To.Add(new MailAddress("atul1502@softwaremerchant.com"));
                message.CC.Add(new MailAddress("Sagar.Galande1119@consoaring.com"));
                message.Subject = "Candidate non eligible PTO";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.sendgrid.net";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("apikey", "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");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 25;
                smtp.Send(message);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}