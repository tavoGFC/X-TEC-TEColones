using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Student
{
    public class ShareTEColonesController : Controller
    {
        // GET: ShareTEColones
        public ActionResult Index()
        {
            return View("~/Views/Student/ShareTEColones/ShareTEColones.cshtml");
        }
    }
}