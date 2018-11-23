using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Controllers
{
    public class LogInController : Controller
    {
        /// <summary>
        /// Get Page Home-WebApp (LogIn)
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        /// <summary>
        /// Return Page Forgot Password
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Pagina para mostrar el recuperar password";
            return View();
        }

        /// <summary>
        /// Get Page Home- Verify User in DB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn()
        {
            AdminModel AdminModel = new AdminModel
            {
                FirstName = "Randy Admin",
                LastName = "Prueba",
                Id = 2010141516,
                
            };
            TempData["admin"] = AdminModel;
            return RedirectToAction("Home", "AdminHome");            
        }

    }
}