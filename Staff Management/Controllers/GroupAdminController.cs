using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Staff_Management.Models; // Adjust namespace as necessary

namespace Staff_Management.Controllers
{
    public class GroupAdminController : Controller
    {



        // GET: 
        public ActionResult Index()
        {
            GroupAdminViewModel TaskList = new GroupAdminViewModel();
            return View(TaskList.ListTasks());

            //return View(ListTasks());

            //return View();
        }

        public ActionResult Tasks() {
            return View();
        }
    }
}