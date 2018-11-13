using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Student
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index() //Models.Student user
        {
            Models.Student user = TempData["mydata"] as Models.Student;
            return View("~/Views/Student/Dashboard/Dashboard.cshtml", user);
        }
    }
}