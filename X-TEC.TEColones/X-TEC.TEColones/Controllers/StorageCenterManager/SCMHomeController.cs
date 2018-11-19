using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.SCMModels;

namespace X_TEC.TEColones.Controllers.StorageCenterManager
{
    public class SCMHomeController : Controller
    {
        

        // GET: SCMHome
        public ActionResult Home()
        {
            SCM scm = (SCM)TempData["scm"];

            return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
        }

        [HttpPost]
        public ActionResult Register() //(SCMModel emple) 
        {
            SCM scm = (SCM)TempData["scm"];
            string carnet = Request["InputCarnet"].ToString();
            //SCMModel emple =  TempData["data"] as SCMModel;
            //System.Diagnostics.Debug.WriteLine("NOMBRE: " + emple.Name);
            //string carnet = Request["InputCarnet"].ToString();
            //emple.Name = "El Barto";
            //System.Diagnostics.Debug.WriteLine("CARNET INGRESADO: " +  InputCarnet);
            //System.Diagnostics.Debug.WriteLine("PYC INGRESADO: " + Request["InputCarnet"]);
            //System.Diagnostics.Debug.WriteLine("PLASTICO INGRESADO: " + InputPlastico);
            //System.Diagnostics.Debug.WriteLine("TETRAPACK INGRESADO: " + InputTetrapack);
            //System.Diagnostics.Debug.WriteLine("VIDRIO INGRESADO: " + InputVidrio);



            return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
        }
    }
}