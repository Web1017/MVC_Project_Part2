using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project_Part2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            //return View();
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Stay connected!";

            //return View();
            return RedirectToAction("Index");
        }
    }
}