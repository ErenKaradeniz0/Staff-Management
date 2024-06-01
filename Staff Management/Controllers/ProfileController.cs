using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Staff_Management.Models;

namespace Staff_Management.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DbContextViewModel _context;

        public ProfileController()
        {
            _context = new DbContextViewModel();
        }

        // Display the profile update form
        [HttpGet]
        public ActionResult ProfilePage()
        {
            if (Convert.ToInt32(Session["UserId"]) == 0)
            {
                TempData["LoginMessage"] = "User not found. You have been redirected.";
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // Handle the form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePage(Users model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Title = model.Title;
                    user.Type = model.Type;

                    _context.SaveChanges();
                    string action = "";

                    switch (user.Type)
                    {
                        case 3:
                            action = "Staff";
                            break;
                        case 2:
                            action = "GroupAdmin";
                            break;
                        case 1:
                            action = "SystemAdmin";
                            break;
                        default:

                            break;
                    }
                    return RedirectToAction("Index", action); // Redirect to home or another page

                }
                ModelState.AddModelError("", "User not found");

            }
            return View(model);
        }
    }
}
