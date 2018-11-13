using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;

namespace X_TEC.TEColones.Controllers.Student
{
    public class ShareTEColonesController : Controller
    {
        // GET: ShareTEColones
        public ActionResult Index() //Models.Student user
        {
            StudentModel user = TempData["mydata"] as StudentModel;
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }


        [HttpPost]
        public ActionResult ShareTCS(StudentModel user)
        {
            string idUser = Request["IdUserToShare"].ToString();
            string tcs = Request["TCSToShare"].ToString();

            user.ShareTCS.TCSToShare = tcs;
            user.ShareTCS.UserToShareId = idUser;
            user.ShareTCS.UserToShereName = "TEST";

            
            user.AssignTCS = new AssignTCSViewModel
            {
                TCS = 1200.5f,

            };
            ViewBag.exists = "Correcto";

            return PartialView("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }


        [HttpPost]
        public ActionResult ApplyTansfer()
        {
            StudentModel user = TempData["mydata"] as StudentModel;
            Console.WriteLine(user.ShareTCS.TCSToShare);
            ViewBag.exists = null;
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }
    }
}