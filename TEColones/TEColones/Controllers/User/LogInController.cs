using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEColones.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {
            string user = Request["IdUser"].ToString();
            return RedirectToAction("Home", "Home", new { user });
        }
    }
}