using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class ConfigurationController : Controller
    {
        // GET: Configuration
        public ActionResult Index()
        {
            return View("~/Views/Administrator/Configuration/Configuration.cshtml");
        }
    }
}