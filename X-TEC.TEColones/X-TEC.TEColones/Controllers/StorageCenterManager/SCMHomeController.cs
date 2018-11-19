using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.SCMModels;
using X_TEC.TEColones.Persistence;

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
            string valuePaper = Request["InputPyC"];
            string valuePlastic = Request["InputPlastico"];
            string valueTetrapack = Request["InputTetrapack"];
            string valueGlass = Request["InputVidrio"];

            if (DBConnection.ExistUser(carnet).Equals(1))
            {
               
                return PartialView("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
            }
            else
            {
                ViewBag.Message = "Usuario no existe, ingrese uno valido";
                return PartialView("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
            }

            //return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
        }
    }
}