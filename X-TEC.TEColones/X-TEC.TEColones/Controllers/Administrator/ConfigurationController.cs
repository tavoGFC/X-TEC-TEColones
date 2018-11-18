using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class ConfigurationController : Controller
    {
     
        /// <summary>
        /// Get page of configuration values of the materials
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialValueConfiguration()
        {
            return View("~/Views/Administrator/Configuration/MaterialConfig.cshtml");
        }

        /// <summary>
        /// /// Get page of configuration values of the TCS
        /// </summary>
        /// <returns></returns>
        public ActionResult TCSValueConfiguration()
        {
            return View("~/Views/Administrator/Configuration/TEColonesConfig.cshtml");
        }

        /// <summary>
        /// Get page of configuration of the credentials of the twitter account
        /// /// </summary>
        /// <returns></returns>
        public ActionResult TwitterConfiguration()
        {
            return View("~/Views/Administrator/Configuration/TwitterConfig.cshtml");
        }

    }
}