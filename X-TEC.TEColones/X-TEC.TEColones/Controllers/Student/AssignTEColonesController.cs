using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;

namespace X_TEC.TEColones.Controllers.Student
{
    public class AssignTEColonesController : Controller
    {
        // GET: AssignTEColones
        public ActionResult Index() //Models.Student user
        {
            StudentModel user = TempData ["mydata"] as StudentModel;
            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }
    }
}