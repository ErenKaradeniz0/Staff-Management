using Staff_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staff_Management.Controllers
{
    public class StaffController : Controller
    {
        private readonly DbContextViewModel _context;

        public StaffController()
        {
            _context = new DbContextViewModel();
        }
        public ActionResult Index()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            var tasks = _context.Tasks.Where(t => t.StaffId == UserId).ToList();
            return View(tasks);
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