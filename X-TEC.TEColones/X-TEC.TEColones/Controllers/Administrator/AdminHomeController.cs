using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Home()
        {
            return View("~/Views/Administrator/AdminHome/AdminHome.cshtml");
        }
    }
}