using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Staff_Management.Models; // Adjust namespace as necessary

public class AccountController : Controller
{
    private readonly DbContextViewModel _context;

    public AccountController()
    {
        _context = new DbContextViewModel();
    }

    [HttpGet]
    public ActionResult Login()
    {
        if (TempData["LoginMessage"] != null)
        {
            ViewBag.LoginMessage = "User not found. You have been redirected.";
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["UserType"] = user.Type;
                
                // Assuming user type is stored as an integer
                switch (user.Type)
                {
                    case 1: // System Admin
                            // Implement authentication and set user session
                        return RedirectToAction("Index", "SystemAdmin");
                    case 2: // Group Admin
                            // Implement authentication and set user session
                        return RedirectToAction("Index", "GroupAdmin");
                    case 3: // Staff
                            // Implement authentication and set user session
                        return RedirectToAction("Index", "Staff");
                    default:
                        ModelState.AddModelError("", "Invalid user type.");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.Message = "Database Connection unsuccessful! " + ex.Message;
            return View(model);
        }
    }

}
