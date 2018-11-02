using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEColones.Controllers.Student
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home(string user)
        {
            ViewBag.Message = "Bienvenido " + user;

            return View();
        }
    }
}