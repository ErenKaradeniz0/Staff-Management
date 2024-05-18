using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staff_Management.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaskDetails()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult ReportIssue()
        {
            return View();
        }
    }
}