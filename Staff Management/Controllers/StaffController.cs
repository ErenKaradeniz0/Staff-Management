using Staff_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        
        [HttpGet]
        public ActionResult TaskDetails(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(task);
        }

        [HttpPost]
        public ActionResult TaskDetails(Tasks taskModel)
        {
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
            return RedirectToAction("TaskDetails", new { id = existingTask.TaskId });
        }
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateTaskStatus(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                task.Status = 2;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public ActionResult ReportIssue(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

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

            return RedirectToAction("ReportIssue", new { id = existingTask.TaskId });
        }
    }
}