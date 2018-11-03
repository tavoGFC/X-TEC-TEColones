using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Student
{
    public class HomeController : Controller
    {
        string _user;
        // GET: Home
        public ActionResult Home(string user)
        {
            _user = user;
            ViewBag.Message = "Bienvenido " + _user;
            return View("~/Views/Student/Home/Home.cshtml");
        }

        
    }
}
