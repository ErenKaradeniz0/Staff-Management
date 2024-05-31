using Staff_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
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
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 3)
            {
                ViewBag.LoginMessage = "User not found. Redirecting to the login page...";
                return View();
            }
            int UserId = Convert.ToInt32(Session["UserId"]);
            var tasks = _context.Tasks.Where(t => t.StaffId == UserId).ToList();
            return View(tasks);
        }
        
        [HttpGet]
        public ActionResult TaskDetails(int? taskId)
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 3)
            {
                ViewBag.LoginMessage = "User not found. Redirecting to the login page...";
                return View();
            }
            else if(taskId == null)
            {
                ViewBag.ErrorMessage = "Task not found. Redirecting to the main page...";
                return View();
            }
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(task);
        }

        [HttpPost]
        public ActionResult TaskDetails(Tasks taskModel)
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 3)
            {
                return RedirectToAction("Login", "Account");
            }
            var existingTask = _context.Tasks.Find(taskModel.TaskId);

            if (existingTask == null)
            {
                // Task not found
                return RedirectToAction("Index", "Error");
            }

            existingTask.Comment = taskModel.Comment;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency conflict
                return View("ConcurrencyError", ex);
            }

            // Redirect to the TaskDetails action with the ID of the updated task
            return RedirectToAction("Index");
        }
        public ActionResult ProfilePage()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 3)
            {
                ViewBag.LoginMessage = "User not found. Redirecting to the login page...";
            }

            return View();
        }

        [HttpPost]
        public ActionResult UpdateTaskStatus(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                task.Status = 2; // Set to 'Completed'
                _context.SaveChanges();
            }

            return RedirectToAction("Index","Staff");
        }
        [HttpGet]
        public ActionResult ReportIssue(int? taskId)
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 3)
            {
                ViewBag.LoginMessage = "User not found. Redirecting to the login page...";
                return View();
            }
            else if (taskId == null)
            {
                ViewBag.ErrorMessage = "Task not found. Redirecting to the main page...";
                return View();
            }


            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(task);
        }

        [HttpPost]
        public ActionResult ReportIssue(Tasks taskModel)
        {
            var existingTask = _context.Tasks.Find(taskModel.TaskId);

            if (existingTask == null)
            {
                return RedirectToAction("Index", "Error");
            }


            existingTask.Comment = taskModel.Comment;
            existingTask.Status = taskModel.Status;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View("ConcurrencyError", ex);
            }

            return RedirectToAction("Index");
        }
    }
}