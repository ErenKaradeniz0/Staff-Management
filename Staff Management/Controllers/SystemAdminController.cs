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
        private readonly DbContextViewModel _context;

        public SystemAdminController()
        {
            _context = new DbContextViewModel();
        }

        public List<Users> ListUser()
        {
            return _context.Users.ToList();

        }
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 1)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(ListUser());
        }
        public ActionResult AssignStaff()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 1)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new AssignStaffViewModel
            {
                AdminList = _context.Users.Where(u => u.Type == 2).ToList(),
                StaffList = _context.Users.Where(u => u.Type == 3).ToList(),

            };
            return View(model);
        }
        public ActionResult CreateEditUser()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0 || Convert.ToInt32(Session["UserType"]) != 1)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(ListUser());
        }

        [HttpPost]
        public ActionResult UpdateUser(Users model)
        {

            if (ModelState.IsValid)
            {
                // Retrieve the user from the database
                var user = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);

                if (user != null)
                {
                    // Update user properties
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Title = model.Title;
                    user.Type = model.Type;

                    // Save changes to the database
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // If model state is not valid or user is not found, redirect to index
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddUser(Users model)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index", "SystemAdmin"); // Redirect to the home page, change as needed
            }
            return RedirectToAction("Index", "SystemAdmin");
        }



        [HttpPost]
        public ActionResult UpdateSalary(int userId, double newSalary)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.Salary = newSalary;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Salary updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "User not found";
            }
            return RedirectToAction("Index");
        }
    }
}