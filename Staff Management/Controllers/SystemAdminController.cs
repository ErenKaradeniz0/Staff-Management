using Staff_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staff_Management.Controllers
{
    public class SystemAdminController : Controller
    {
        public List<Users> ListUser()
        {
            using (var _content = new DbContextViewModel())
            {
                return _content.Users.ToList();
            }

        }

        // GET: SystemAdmin
        public ActionResult Index()
        {
            
            return View(ListUser());

            //return View();
        }
        public ActionResult AdjustSalaries()
        {
            return View();
        }
        public ActionResult AssignStaff()
        {
            return View();
        }
    }
}