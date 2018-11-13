using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;

namespace X_TEC.TEColones.Controllers.Student
{
    public class ShareTEColonesController : Controller
    {
        // GET: ShareTEColones
        public ActionResult Index() //Models.Student user
        {
            Models.Student user = TempData["mydata"] as Models.Student;
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }


        [HttpPost]
        public ActionResult ShareTCS(Models.Student user)
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
            Models.Student user = TempData["mydata"] as Models.Student;
            Console.WriteLine(user.ShareTCS.TCSToShare);
            ViewBag.exists = null;
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml", user);
        }
    }
}