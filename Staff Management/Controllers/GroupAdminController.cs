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
        private readonly DbContextViewModel _context;

        public GroupAdminController()
        {
            _context = new DbContextViewModel();
        }

        // GET: 
        public ActionResult Index()
        {
            int UserID = Convert.ToInt32(Session["UserID"]);

            // Query to fetch the data with the filter condition
            var query = from assignment in _context.Assignments
                        join user in _context.Users on assignment.StaffId equals user.UserId
                        join task in _context.Tasks on user.UserId equals task.StaffId
                        where assignment.GroupAdminId == UserID // Filter condition
                        select new GroupAdminViewModel
                        {
                            TaskId = task.TaskId,
                            TaskStatus = task.Status,
                            TaskTitle = task.Title,
                            UserId = user.UserId,
                            UserName = user.Name,
                            UserSurname = user.Surname
                        };

            // Convert the query result to a list
            List<GroupAdminViewModel> TaskList = query.ToList();

            // Pass the list to the view
            return View(TaskList);
        }

        public ActionResult Tasks(int viewTaskId) {
             

            int UserID = Convert.ToInt32(Session["UserID"]);

            var query = from assignment in _context.Assignments
                      join user in _context.Users on assignment.StaffId equals user.UserId
                      where assignment.GroupAdminId == UserID // Filter condition
                      select new GroupAdminViewModel
                      {
                          UserId = user.UserId,
                          UserName = user.Name,
                          UserSurname = user.Surname
                      };

            // Convert the query result to a list
            List<GroupAdminViewModel> ListGroupStaff = query.ToList();

            var query2 = from  task in _context.Tasks 
                        
                        where task.TaskId == viewTaskId // Filter condition
                        select new GroupAdminViewModel
                        {
                            StaffId = task.StaffId,
                            TaskContent =task.Contents,
                            TaskComment=task.Comment
                            
                        };
            List<GroupAdminViewModel> TaskList = query2.ToList();

            GroupAdminViewModel model = new GroupAdminViewModel
            {
                ListGroupStaff = ListGroupStaff,
                ListTasks = TaskList
            };

            return View(model);
        }
        
        


    }
}