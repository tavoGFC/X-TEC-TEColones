using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminHomeController : Controller
    {
        string _user;
        // GET: AdminHome
        public ActionResult Home()
        {
           // _user = user;
            ViewBag.Message = "Bienvenido ";
            return View("~/Views/Administrator/AdminHome/AdminHome.cshtml");
        }
    }
}