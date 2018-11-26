using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class CreateUserController : Controller
    {
        // GET: CreateUser
        public ActionResult Index()
        {
            AdminModel AdminModel = (AdminModel)TempData["admin"];
            AdminModel.NewAdminSCMModel = new NewAdminSCM();
            return View("~/Views/Administrator/CreateUser/CreateUser.cshtml", AdminModel);
        }


        [HttpPost]
        public ActionResult CreateUser(AdminModel adminModelNewUser)
        {
            AdminModel adminModel = (AdminModel)TempData["admin"];
            adminModel.NewAdminSCMModel = adminModelNewUser.NewAdminSCMModel;

            int isAdmin = int.Parse(Request["isAdmin"].ToString());

            adminModel.NewAdminSCMModel.SetValues();

            if (DBConnection.InsertAdminSCM(adminModel.NewAdminSCMModel, isAdmin))
            {

                ViewBag.Message = "Se a completado con exito el registro del nuevo usuario";
            }
            else
            {
                ViewBag.Message = "Existe un usuario con el mismo numero de cedula, verifique por favor";
            }

            
            return View("~/Views/Administrator/CreateUser/CreateUser.cshtml", adminModel);
        }
    }
}