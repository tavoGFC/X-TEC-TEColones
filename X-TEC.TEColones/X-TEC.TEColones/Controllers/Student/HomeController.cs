using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;

namespace X_TEC.TEColones.Controllers.Student
{
    public class HomeController : Controller
    {

        //GET:Home
        public ActionResult Home(Models.Student userS)
        {
            userS.AssignTCS = new AssignTCSViewModel
            {
                TCS = 1200.5f,
                ExrDinningRoom = 0.80f,
                ExrEnrollment = 0.35f

            };
            userS.ShareTCS = new ShareTCSViewModel
            {
                TCSToShare = " ",
                UserToShareId = " ",
                UserToShereName = " "

            };
            return View("~/Views/Student/Home/Home.cshtml", userS);
        }

        
    }
}
