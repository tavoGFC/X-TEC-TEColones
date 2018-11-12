using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;

namespace X_TEC.TEColones.Controllers.Student
{
    public class HomeController : Controller
    {

        //GET:Home
        public ActionResult Home(StudentModel user)
        {
            ViewBag.Message = "Bienvenido " +user.Identification;
            return View("~/Views/Student/Home/Home.cshtml");
        }

        
    }
}
