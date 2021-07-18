using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackCandidate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Index()
        {
          
            return View("Dashboard");
        }
        public ActionResult Candidate()
        {

            return View();
        }
        public ActionResult Vendor()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult InvoiceReport()
        {
            return View();
        }
        public ActionResult InvoiceAutoMailSetting()
        {
            return View();
        }
    }
}
