using System;
using System.Linq;
using System.Net.Mail;
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
            string encryptedPassword = PasswordEncrypt(model.Password);
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == encryptedPassword);
            if (user != null)
            {
                Session["UserId"] = user.UserId;
                Session["UserType"] = user.Type;

                // Assuming user type is stored as an integer
                switch (user.Type)
                {
                    case 1: // System Admin
                        return RedirectToAction("AdjustSalaries", "SystemAdmin");
                    case 2: // Group Admin
                        return RedirectToAction("ListTasks", "GroupAdmin");
                    case 3: // Staff
                        return RedirectToAction("Index", "Staff");
                    default:
                        ModelState.AddModelError("", "Invalid user type.");
                        return View(model);
                }
            }
            else
            {
                ViewBag.LoginMessage = "Invalid username or password.";
                return View();
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.Message = "Database Connection unsuccessful! " + ex.Message;
            return View(model);
        }
    }
    public ActionResult Logout()
    {
        Session.Clear(); // Clear all session data
        Session.Abandon(); // End the session
        return RedirectToAction("Login", "Account");
    }

    public static string PasswordEncrypt(string password)
    {
        int minAsciiVal = 32;
        int maxAsciiVal = 126;
        int key = 13;
        int mirrorKey = -17;
        string crypto = "";
        string cryptoMirror = "";
        string mirror = new string(password.Reverse().ToArray());

        for (int i = 0; i < password.Length; i++)
        {
            int charPassword = (int)password[i] + key;
            int charMirror = (int)mirror[i] + mirrorKey;
            if (charPassword > maxAsciiVal)
                charPassword = charPassword - maxAsciiVal + minAsciiVal - 1;
            if (charMirror < minAsciiVal)
                charMirror = charMirror + maxAsciiVal - minAsciiVal + 1;
            // Escape single quotes in the password
            if (charPassword == 39)
            {
                charPassword = int.Parse("39" + charPassword);
            }
            if (charMirror == 39)
            {
                charMirror = int.Parse("39" + charMirror);
            }
            // Escape double quotes in the password
            if (charPassword == 34)
            {
                charPassword = int.Parse("34" + charPassword);
            }
            if (charMirror == 34)
            {
                charMirror = int.Parse("34" + charMirror);
            }
            crypto += (char)charPassword;
            cryptoMirror += (char)charMirror;
        }
        string newPassword = crypto + cryptoMirror;
        return newPassword;
    }
    private string GenerateRandomPassword()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    private bool SendPasswordResetEmail(string email, string newPassword)
    {

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("staffmanagement.eren@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Password Reset";

            var client = _context.Users.FirstOrDefault(u => u.Email == email);
            mail.Body = "Hello " + client.Name + " " + client.Surname + ", Your new password: " + newPassword;
         

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("staffmanagement.eren@gmail.com", "eemcpyvzyadshliu");


            smtpClient.Send(mail);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    [HttpGet]
    public ActionResult Forgot_Password()
    {

        return View();
    }

    [HttpPost]
    public ActionResult Forgot_Password(string email)
    {
        string newPassword = GenerateRandomPassword();
        string encryptedPassword = PasswordEncrypt(newPassword);


        var client = _context.Users.FirstOrDefault(u => u.Email == email);

        if (client != null)
        {
            if (SendPasswordResetEmail(email, newPassword))
            {
                client.Password = encryptedPassword;
                _context.SaveChanges();
                ViewBag.SuccessMessage = "A new password has been sent to your email.";
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to send the new password. Please try again.";
            }
        }
        else
        {
            ViewBag.ErrorMessage = "You do not have an account.";
        }


        return View();

    }

}
