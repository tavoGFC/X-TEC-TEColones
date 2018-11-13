using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;

namespace X_TEC.TEColones.Controllers.Student
{
    public class AssignTEColonesController : Controller
    {
        // GET: AssignTEColones
        public ActionResult Index() //Models.Student user
        {
            Models.Student user = TempData ["mydata"] as Models.Student;
            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }
    }
}