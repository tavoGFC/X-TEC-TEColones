using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;
using X_TEC.TEColones.Persistence;

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
            string user = Request["IdUser"].ToString();
            string password = Request["PasswordUser"].ToString();

            int typeUser = DBConnection.ExistUser(user);
            string message = string.Empty;

            switch (typeUser)
            {
                case 1:
                    StudentModel student = DBConnection.VerifyStudent(user, password);
                    if (student.Id != 0)
                    {
                        return RedirectToAction("Home", "Home", student);
                    }
                    message = "Verifique los datos ingresados incorrecto";
                    break;

                case 2:
                    
                case 0:
                    message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
                    break;
            }
            return LogIn(message);
            
        }
    }
}