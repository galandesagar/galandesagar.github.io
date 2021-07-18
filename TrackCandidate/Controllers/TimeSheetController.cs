using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using TrackCandidate.Models;
using TrackCandidate.Services;

namespace TrackCandidate.Controllers
{
    public class TimeSheetController : ApiController
    {
        private readonly TimesheetService _timesheetService;
        public TimeSheetController()
        {
            _timesheetService =new TimesheetService();
        }
        // GET: api/TimeSheet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       
        [HttpPost]
        [Route("api/TimeSheet/AddTimeSheet")]
        public void Post(AddTimesheetDTO addTimeSheetDTO)
        {
            _timesheetService.AddTimeSheet(addTimeSheetDTO);
        }

        [HttpPost]
        public  async void Post()
        {
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    var CandidateId =httpContext.Request.Form["CandidateId"];
                    var TimeCardId = httpContext.Request.Form["TimeCardId"];
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    string storageConnection = "DefaultEndpointsProtocol=https;AccountName=smtimesheet;AccountKey=f2lO1ZWymMyDQaAftoEqKsTMaCcI/nGiUqS7zyG7Z0fzPwgYFrNMWjB6KeJbVQISKCc+klU472Up65vt4NbMMg==;EndpointSuffix=core.windows.net";
                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);

                    //create a block blob 
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                    //create a container 
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("timesheet");

                    //create a container if it is not already exists

                    if (await cloudBlobContainer.CreateIfNotExistsAsync())
                    {

                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    }

                    string imageName =  CandidateId + "_" + TimeCardId + "_" + httpPostedFile.FileName;

                    //get Blob reference

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName); cloudBlockBlob.Properties.ContentType = httpPostedFile.ContentType;

                    await cloudBlockBlob.UploadFromStreamAsync(httpPostedFile.InputStream);
                   
                }
            }

        }

        [HttpPost]
        [Route("api/Timesheet/SaveInvoiceAutoMailSetting")]
        public void SaveInvoiceAutoMailSetting(int Id, string Reminder, int NoOfDays)
        {
            _timesheetService.SaveTimesheetAutoMailReminderSetting(Id, Reminder, NoOfDays);
        }

        [HttpGet]
        [Route("api/Timesheet/GetTimeSheetAutoMailReminderSetting")]
        public IEnumerable<TimesheetAutoMailRemindercs> GetTimeSheetAutoMailReminderSetting()
        {
            var Result = _timesheetService.GetTimesheetAutoMailReminderSetting();
            return Result;
        }

        [HttpPost]
        [Route("api/Timesheet/AddWeekdate")]
        public HttpResponseMessage AddWeekDate( int MonthId)
        {
             _timesheetService.AddWeekDates(MonthId);
           
            return Request.CreateResponse(HttpStatusCode.OK, "Success");

        }
    }
}
