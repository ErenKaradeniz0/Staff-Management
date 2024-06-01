﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 2)
            {
                TempData["LoginMessage"] = "User not found. You have been redirected.";
                return RedirectToAction("Login", "Account");
            }
            else if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            int GroupAdminId = Convert.ToInt32(Session["UserId"]);
            var GroupAdmin = _context.Users.Find(GroupAdminId);

            var query = from assignment in _context.Assignments
                        join user in _context.Users on assignment.StaffId equals user.UserId
                        where assignment.GroupAdminId == GroupAdminId // Filter condition
                        select new GroupAdminViewModel
                        {
                            StaffId = user.UserId,
                            StaffName = user.Name,
                            StaffSurname = user.Surname
                        };

            // Convert the query result to a list
            List<GroupAdminViewModel> TaskList = query.ToList();

            var query2 = from assignment in _context.Assignments
                         join user in _context.Users on assignment.StaffId equals user.UserId
                         join task in _context.Tasks on user.UserId equals task.StaffId
                         where assignment.GroupAdminId == GroupAdminId
                         select new GroupAdminViewModel
                         {
                             StaffId = user.UserId,
                             StaffName = user.Name,
                             StaffSurname = user.Surname,
                             TaskId = task.TaskId,
                             Title = task.Title,
                             Status = task.Status
                         };



            List<GroupAdminViewModel> ListGroupStaff = query2.ToList();
            GroupAdminViewModel model = new GroupAdminViewModel
            {
                ListGroupStaff = ListGroupStaff,
                ListTasks = TaskList,
            };
            // Pass the list to the view
            return View(model);
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

            return RedirectToAction("Index", "GroupAdmin");
        }

        [HttpGet]
        public ActionResult Tasks(int? taskId)
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 2)
            {
                TempData["LoginMessage"] = "User not found. You have been redirected.";
                return RedirectToAction("Login", "Account");
            }
            else if (taskId == null)
            {
                TempData["ErrorMessage"] = "Task not found. You have been redirected.";
                return RedirectToAction("Index", "GroupAdmin");
            }
            var existingTask = _context.Tasks.Find(taskId);
            var existingUser = _context.Users.Find(existingTask.StaffId);


            int UserId = Convert.ToInt32(Session["UserId"]);

            var query = from assignment in _context.Assignments
                        join user in _context.Users on assignment.StaffId equals user.UserId
                        where assignment.GroupAdminId == UserId // Filter condition
                        select new GroupAdminViewModel
                        {
                            StaffId  = user.UserId,
                            StaffName = user.Name,
                            StaffSurname = user.Surname
                        };

            // Convert the query result to a list
            List<GroupAdminViewModel> ListGroupStaff = query.ToList();

            var query2 = from task in _context.Tasks
                         where task.TaskId == taskId // Filter condition
                         select new GroupAdminViewModel
                         {
                             StaffId = task.StaffId,
                             Contents = task.Contents,
                             Comment = task.Comment

                         };
            List<GroupAdminViewModel> TaskList = query2.ToList();

            GroupAdminViewModel model = new GroupAdminViewModel
            {
                ListGroupStaff = ListGroupStaff,
                ListTasks = TaskList,
                TaskId = (int)taskId,
                Title = existingTask.Title,
                Status = existingTask.Status,
                StaffId = existingTask.StaffId,
                StaffName = existingUser.Name,
                StaffSurname = existingUser.Surname,
                Comment = existingTask.Comment,
                Contents = existingTask.Contents,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Tasks(GroupAdminViewModel groupAdminTaskModel)
        {
            var existingTask = _context.Tasks.Find(groupAdminTaskModel.TaskId);
            if (existingTask == null)
            {
                return RedirectToAction("Index", "Error");
            }
            existingTask.Title = groupAdminTaskModel.Title;
            existingTask.Contents = groupAdminTaskModel.Contents;
            existingTask.Comment = groupAdminTaskModel.Comment;
            existingTask.StaffId = groupAdminTaskModel.StaffId;

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


        [HttpPost]
        public ActionResult AddTask(Tasks model)
        {
            if (ModelState.IsValid)
            {
                var newTask = new Tasks
                {
                    Title = model.Title,
                    Contents = model.Contents,
                    Comment = model.Comment,
                    Status = model.Status,
                    StaffId = model.StaffId
                };

                _context.Tasks.Add(newTask);
                _context.SaveChanges();

                return RedirectToAction("Index"); // Redirect to a relevant action after saving
            }

            // If model state is not valid, return to the form with the current model to show validation errors
            //model.ListGroupStaff = _context.Staff.ToList(); // Assuming you need to reload the staff list
            return View(model);
        }
    }
    

}