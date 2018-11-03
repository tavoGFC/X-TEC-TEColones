using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.StorageCenterManager
{
    public class SCMHomeController : Controller
    {
        // GET: SCMHome
        public ActionResult Home()
        {
            return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml");
        }
    }
}