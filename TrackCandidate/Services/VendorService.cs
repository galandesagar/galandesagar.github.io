using Priority_Express_Service.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrackCandidate.Models;

namespace TrackCandidate.Services
{
    public class VendorService
    {
        private readonly SqlServerRepository _sqlServerRepository;
        public VendorService()
        {
            _sqlServerRepository = new SqlServerRepository();
        }
        public int AddVendor(AddVendorDTO addVendorDTO)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorName", addVendorDTO.VendorName));
            parameter.Add(new SqlParameter("@PaymentTerm", addVendorDTO.PaymentTerm));
            parameter.Add(new SqlParameter("@StartDate", addVendorDTO.StartDate));
            parameter.Add(new SqlParameter("@EndDate", addVendorDTO.EndDate));
            parameter.Add(new SqlParameter("@MailTo", addVendorDTO.MailTo));
            parameter.Add(new SqlParameter("@MailCc", addVendorDTO.MailCc));
            parameter.Add(new SqlParameter("@MailBcc", addVendorDTO.MailBcc));
            parameter.Add(new SqlParameter("@InvoiceType", addVendorDTO.InvoiceType));
            parameter.Add(new SqlParameter("@ContactPerson", addVendorDTO.ContactPerson));

            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "AddVendor";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32( parameter["@rowCount"].Value);
            return result;
        }

        public int EditVendor(EditVendorDTO editVendorDTO)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorId", editVendorDTO.VendorId));
            parameter.Add(new SqlParameter("@VendorName", editVendorDTO.VendorName));
            parameter.Add(new SqlParameter("@PaymentTerm", editVendorDTO.PaymentTerm));
            parameter.Add(new SqlParameter("@StartDate", editVendorDTO.StartDate));
            parameter.Add(new SqlParameter("@EndDate", editVendorDTO.EndDate));
            parameter.Add(new SqlParameter("@MailTo", editVendorDTO.MailTo));
            parameter.Add(new SqlParameter("@MailCc", editVendorDTO.MailCc));
            parameter.Add(new SqlParameter("@MailBcc", editVendorDTO.MailBcc));
            parameter.Add(new SqlParameter("@InvoiceType", editVendorDTO.InvoiceType));
            parameter.Add(new SqlParameter("@ContactPerson", editVendorDTO.ContactPerson));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "EditVendor";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32(parameter["@rowCount"].Value);
            return result;
        }

        public List<Vendor> GetVendor()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetVendor";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<Vendor>(sqlresult);
            var result = parameter["@rowCount"].Value.ToString();
            return output;
        }

        public int DeleteVendor(int VendorId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@VendorId", VendorId));
            parameter.Add(new SqlParameter("@rowCount", SqlDbType.Int));
            parameter["@rowCount"].Direction = ParameterDirection.Output;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "DeleteVendor";
            var sqlresult = _sqlServerRepository.ExecuteNonQuery(Command);
            var result = Convert.ToInt32(parameter["@rowCount"].Value);
            return result;
        }

    }
}