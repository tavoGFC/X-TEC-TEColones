using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Student
{
    public class AssignTEColonesController : Controller
    {
        // GET: AssignTEColones
        public ActionResult Index() //Models.Student user
        {
            StudentModel user = (StudentModel)TempData["student"];
            DBConnection.GetBenefit(user);
            user.AssignTCS.SetExchanRate();
            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }


        [HttpPost]
        public ActionResult AssignTCS_Comedor()
        {
            StudentModel user = (StudentModel)TempData["student"];
            int tcsToAssign = Int32.Parse(Request["TCSToAssgin"]);
            float colones = user.AssignTCS.ExRDinningRoom * tcsToAssign;

            if(DBConnection.InsertLogAssign(user.Id, "Comedor", tcsToAssign, colones, user.AssignTCS.ExRDinningRoom))
            {
                user.TCS -= tcsToAssign;
            }

            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }

        [HttpPost]
        public ActionResult AssignTCS_Matricula()
        {
            StudentModel user = (StudentModel)TempData["student"];
            int tcsToAssign = Int32.Parse(Request["TCSToAssgin1"]);
            float colones = user.AssignTCS.ExREnrollment * tcsToAssign;

            if (DBConnection.InsertLogAssign(user.Id, "Matricula", tcsToAssign, colones, user.AssignTCS.ExREnrollment))
            {
                user.TCS -= tcsToAssign;
            }

            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }
    }
}