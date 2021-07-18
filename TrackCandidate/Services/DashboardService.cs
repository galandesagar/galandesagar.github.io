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
    public class DashboardService
    {
        private readonly SqlServerRepository _sqlServerRepository;
        public DashboardService()
        {
            _sqlServerRepository = new SqlServerRepository();
        }
        public List<WeeklyDetails> GetWeeklyDetails()
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "GetWeeklyDetails_Dashboard";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<WeeklyDetails>(sqlresult);
            return output;
        }
        public List<WeeklyDetails> GetWeeklyfilterDetails(int VendorId,int CandidateId,int MonthId,int YearId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandidateId", CandidateId));
            parameter.Add(new SqlParameter("@vendorId", VendorId));
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            parameter.Add(new SqlParameter("@YearId", YearId));

            Command.CommandText = "GetWeeklyDetailsFilter_Dashboard";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<WeeklyDetails>(sqlresult);
            return output;
        }

        public List<LeavesDetail> GetLeavesDetails(int CandId, int YearId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandId", CandId));
            parameter.Add(new SqlParameter("@YearId", YearId));

            Command.CommandText = "GetLeavesDetails";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<LeavesDetail>(sqlresult);
            return output;
        }

        public List<Leavesbifurcation> getLeavesbifurcation(int CandId, int TypeId, int MonthId,int YearId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandId", CandId));
            parameter.Add(new SqlParameter("@TypeId", TypeId));
            parameter.Add(new SqlParameter("@MonthId", MonthId));
            parameter.Add(new SqlParameter("@YearId", YearId));

            Command.CommandText = "GetLeavesbifurcation";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<Leavesbifurcation>(sqlresult);
            return output;
        }


        public List<LeavesCalculation> getLeavesCalculation(int CandId, int YearId)
        {
            var Command = new SqlCommand();
            var parameter = Command.Parameters;
            parameter.Add(new SqlParameter("@CandId", CandId));
            parameter.Add(new SqlParameter("@Year", YearId));

            Command.CommandText = "GetLeavesCalculation";
            var sqlresult = _sqlServerRepository.ExecuteStoredProcedure(Command);
            var output = Helper.DataTableToClass<LeavesCalculation>(sqlresult);
            return output;
        }
    }
}