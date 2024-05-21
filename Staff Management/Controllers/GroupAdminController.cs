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
        public List<TaskEventHandler> ListTasks()
        {
            using (var ctx = new DbContextViewModel())
            {
                var query = from Tasks in ctx.Tasks
                            join Users in ctx.Users
                            on Tasks.StaffId equals Users.UserId
                            select new TaskEventHandler(
                                Tasks.Status,
                                Tasks.Title,
                                Users.Name,
                                Users.Surname
                            );

                return query.AsEnumerable().Select(x => new
                {
                    Status = x.Status,
                    Title = x.Title,
                    StaffName = $"{x.Name} {x.Surname}"
                }).ToList<TaskEventHandler>();
            }
        }


        // GET: 
        public ActionResult Index()
        {
            //Tasks TaskList = new Tasks();
            //return View(TaskList.ListTasks());

            return View(ListTasks());

            //return View();
        }

        public ActionResult Tasks() {
            return View();
        }
    }
}