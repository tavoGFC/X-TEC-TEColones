using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Student
{
    public class ShareTEColonesController : Controller
    {
        // GET: ShareTEColones
        public ActionResult Index() //Models.Student user
        {
            ViewBag.Message = "";
            StudentModel user = (StudentModel)TempData["student"];
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }


        [HttpPost]
        public ActionResult ShareTCS()
        {
            StudentModel user = (StudentModel)TempData["student"];
            string idUser = Request["IdUserToShare"].ToString();
            string tcsString = Request["TCSToShare"].ToString();
           
            if (DBConnection.ExistUser(idUser).Equals(1)){
                user.ShareTCS.TCSToShare = tcsString;
                user.ShareTCS.UserToShareId = idUser;
                user.ShareTCS.UserToShereName = DBConnection.GetNameUser(idUser);
                ViewBag.exists = "exito";
                return PartialView("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);              
            }
            else
            {
                ViewBag.exists = null;
                ViewBag.Message = "Usuario no existe, ingrese uno valido";
                return PartialView("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
            }
        }


        [HttpPost]
        public ActionResult ApplyTansfer()
        {
            StudentModel user = (StudentModel)TempData["student"];

            if (DBConnection.ShareTCS(user))
            {
                int tcs = Int32.Parse(user.ShareTCS.TCSToShare);
                user.TCS -= tcs;
                ViewBag.exists = null;
                ViewBag.Message = "";
                user.ShareTCS.TCSToShare = "0";
                user.ShareTCS.UserToShareId = "0";
                return PartialView("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
            }
            else
            {
                ViewBag.exists = null;
                ViewBag.Message = "Por favor intente de nuevo";
                return PartialView("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
            }            
        }

    }
}