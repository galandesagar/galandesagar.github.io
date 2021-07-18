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
    public class CandidateController : ApiController
    {
        private readonly CandidateService _candidateService;
        public CandidateController()
        {
            _candidateService = new CandidateService();
        }
       
        [HttpGet]
        [Route("api/candidate/getcandidate")]
        public IEnumerable<Candidate> Get(int MonthId,int YearId,int StatusId)
        {
            return _candidateService.GetCandidate(MonthId,YearId,StatusId);
        }

        [HttpPost]
        [Route("api/candidate/addcandidate")]
        public HttpResponseMessage Post(AddCandidateDTO addCandidateDTO)
        {
           var result = _candidateService.AddCandidate(addCandidateDTO);
            if(result != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Success");

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Duplicate check candidate name");

            }
        }

        [HttpPost]
        [Route("api/candidate/updatecandidate")]
        public HttpResponseMessage updatecandidate(EditCandidateDTO editCandidateDTO)
        {
            var result = _candidateService.EditCandidate(editCandidateDTO);
            if (result != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Record not exist");
            }
        }


        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("api/candidate/deletecandidate")]
        public HttpResponseMessage Delete(int CandidateId)
        {
            var result =_candidateService.DeleteCandidate(CandidateId);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "fail");
            }
        }

        [HttpGet]
        [Route("api/candidate/getcandidateDetails")]
        public IEnumerable<Candidate> candidateDetails()
        {
            return _candidateService.GetcandidateDetails();
        }

        [HttpGet]
        [Route("api/candidate/getclientholidaylist")]
        public IEnumerable<ClientHoliday> getClientHolidayList()
        {
            return _candidateService.getClientHolidayList();
        }
        [HttpPost]
        [Route("api/candidate/addClientHoliday")]
        public void addClientHoliday(string HolidayName,string Date)
        {
        _candidateService.addClientHoliday(HolidayName, Date);
          
        }

        [HttpGet]
        [Route("api/candidate/getcandidatePTODetails")]
        public IEnumerable<CandidatePTODetails> candidatePTODetails(int MonthId, int YearId, int CandId)
        {
            return _candidateService.candidatePTODetails(MonthId, YearId, CandId);
        }

        [HttpGet]
        [Route("api/candidate/SendPTOMail")]
        public bool SendPTOMail(string CandName, int PTOAvbl,int PTOTaken, string comment)
        {
            return _candidateService.SendPTOMail(CandName, PTOAvbl, PTOTaken, comment);
        }
    }
}
