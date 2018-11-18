using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;

namespace X_TEC.TEColones.Controllers.Student
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index() //Models.Student user
        {
            StudentModel user = TempData["student"] as StudentModel;
            return View("~/Views/Student/Dashboard/Dashboard.cshtml", user);
        }
    }
}