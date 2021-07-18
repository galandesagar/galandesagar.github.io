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
    public class VendorController : ApiController
    {
        private readonly VendorService _verdorService;
        public VendorController()
        {
            _verdorService = new VendorService();
        }
        
        [HttpGet]
        [Route("api/vendor/getvendor")]
        public IEnumerable<Vendor> Get()
        {
           return _verdorService.GetVendor();
        }


        [HttpPost]
        [Route("api/vendor/addvendor")]
        public HttpResponseMessage Post(AddVendorDTO addVendorDTO)
        {
           var result = _verdorService.AddVendor(addVendorDTO);
            if(result != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = "success" });
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate check vendor name");
            }
        }

        [HttpPost]
        [Route("api/vendor/updatevendor")]
        public HttpResponseMessage Update(EditVendorDTO editVendorDTO)
        {
            var result = _verdorService.EditVendor(editVendorDTO);
            if (result != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result = "success" });
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Record not Exists");
            }
        }

        [HttpDelete]
       [Route("api/vendor/vendordelete")]
        public HttpResponseMessage Delete(int VendorId)
        {
           var result = _verdorService.DeleteVendor(VendorId);
            if(result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,"success");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "fail");
            }
        }

       
    }
}
