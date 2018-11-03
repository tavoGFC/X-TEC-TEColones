using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Student
{
    public class AssignTEColonesController : Controller
    {
        // GET: AssignTEColones
        public ActionResult Index()
        {
            return View("~/Views/Student/AssignTEColones/AssignTEColones.cshtml");
        }
    }
}