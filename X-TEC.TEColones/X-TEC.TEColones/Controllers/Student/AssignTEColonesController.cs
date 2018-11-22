using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Data;
using System.Configuration;

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

            if (user.TCS != 0)
            {

                int tcsToAssign = Int32.Parse(Request["TCSToAssgin"]);
                float colones = user.AssignTCS.ExRDinningRoom * tcsToAssign;

                if (DBConnection.InsertLogAssign(user.Id, "Comedor", tcsToAssign, colones, user.AssignTCS.ExRDinningRoom))
                {
                    user.TCS -= tcsToAssign;
                    CreateJson(user.Id, "Comedor", colones);
                }
            }
            ViewBag.Error = "No cuenta con suficientes TEColones";
            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }

        [HttpPost]
        public ActionResult AssignTCS_Matricula()
        {
            StudentModel user = (StudentModel)TempData["student"];
            if (user.TCS != 0)
            {
                int tcsToAssign = Int32.Parse(Request["TCSToAssgin1"]);
                float colones = user.AssignTCS.ExREnrollment * tcsToAssign;

                if (DBConnection.InsertLogAssign(user.Id, "Matricula", tcsToAssign, colones, user.AssignTCS.ExREnrollment))
                {
                    user.TCS -= tcsToAssign;
                    CreateJson(user.Id, "Matricula", colones);
                }
            }
            ViewBag.Error = "No cuenta con suficientes TEColones";
            return PartialView("~/Views/Student/AssignTEColones/AssignTEColones.cshtml", user);
        }


        public void CreateJson(int id, string benefit, float amount)
        {
            try
            {
                string json = "{" + String.Format(@"carnet: {0}, asignacion: [", id) + "{" 
                    +String.Format(@"beneficio: '{0}',colones: {1}", benefit.ToString(), amount) + "}]}";

                JObject objetJson = JObject.Parse(json);
                string pathFile = ConfigurationManager.AppSettings["pathFiles"].ToString();
                using (StreamWriter file = new StreamWriter(pathFile + id + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, objetJson);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error CreateJson " + ex.Message);
            }
                     
        }

    }
}