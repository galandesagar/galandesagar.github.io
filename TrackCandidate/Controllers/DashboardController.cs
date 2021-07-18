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
    public class DashboardController : ApiController
    {
        private readonly DashboardService _dashboardService;
        public DashboardController()
        {
            _dashboardService = new DashboardService();
        }
        [HttpGet]
        [Route("api/Dashboard/getweeklydetails")]
        public IEnumerable<WeeklyDetails> Get()
        {
            return _dashboardService.GetWeeklyDetails();
        }


        [HttpGet]
        [Route("api/Dashboard/getweeklyfilterdetails")]
        public IEnumerable<WeeklyDetails> getweeklyfilterdetails(int VendorId,int CandidateId,int MonthId,int YearId)
        {
            return _dashboardService.GetWeeklyfilterDetails(VendorId, CandidateId,MonthId,YearId);
        }

        [HttpGet]
        [Route("api/Dashboard/getLeavesDetails")]
        public IEnumerable<LeavesDetail> getLeavesDetails(int CandId, int YearId)
        {
            return _dashboardService.GetLeavesDetails(CandId,YearId);
        }
        [HttpGet]
        [Route("api/Dashboard/getLeavesbifurcation")]
        public IEnumerable<Leavesbifurcation> getLeavesbifurcation(int CandId, int TypeId, int MonthId,int YearId)
        {
            return _dashboardService.getLeavesbifurcation(CandId, TypeId, MonthId, YearId);
        }

        [HttpGet]
        [Route("api/Dashboard/getLeavesCalculation")]
        public IEnumerable<LeavesCalculation> getLeavesCalculation(int CandId, int YearId)
        {
            return _dashboardService.getLeavesCalculation(CandId, YearId);
        }
        // GET: api/Dashboard/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dashboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
        }
    }
}
