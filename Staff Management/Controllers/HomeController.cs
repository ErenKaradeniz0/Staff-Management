using Staff_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Staff_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContextViewModel _context;

        public HomeController()
        {
            _context = new DbContextViewModel();
        }
        public ActionResult Index()
        {
            
            return View();
        }
    }
}