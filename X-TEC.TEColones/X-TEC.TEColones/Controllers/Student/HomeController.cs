using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Student
{
    public class HomeController : Controller
    {

        //GET:Home
        public ActionResult Home()
        {
            StudentModel user = (StudentModel)TempData["student"];
            DBConnection.GetBenefit(user);
            DBConnection.GetMaterial(user);
            return View("~/Views/Student/Home/Home.cshtml", user);
        }

        
    }
}
