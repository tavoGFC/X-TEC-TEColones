using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers
{
    public class LogInController : Controller
    {
        /// <summary>
        /// Get Page Home-WebApp (LogIn)
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View("~/Views/StorageCenterManager/miECAHome/miECAHome.cshtml");
        }

        /// <summary>
        /// Return Page Forgot Password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        /// <summary>
        /// Get Page Home- Verify User in DB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            string user = Request["IdUser"].ToString();
            if (user.Equals("admin"))
            {
                return RedirectToAction("Home", "AdminHome");
            }
            else if (user.Equals("eca"))
            {
                return RedirectToAction("Home", "SCMHome");
            }
            else
            {
                return RedirectToAction("Home", "Home", new { user });
            }
        }
    }
}