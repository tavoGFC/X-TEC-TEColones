using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Models.SCMModels;
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
                        if (student.PhotoBytes.Count() == 0)
                        {
                            student.Photo = student.DefaultPhoto();
                        }
                        else
                        {
                            student.RenderImage();
                        }
                        TempData["student"] = student;
                        return RedirectToAction("Home", "Home");
                    }
                    message = "Verifique los datos ingresados son incorrectos";
                    break;

                case 2:
                    var user_Id = DBConnection.VerifyAdminSCM(user, password);
                    if (user_Id.Item1 != 0)
                    {
                        //admin
                        if (user_Id.Item2)
                        {
                            //falta agregar lo del admin
                            AdminModel adminModel = new AdminModel()
                            {
                                FirstName = "Genesis",
                                LastName = "Adam",
                                
                            };
                            TempData["admin"] = adminModel;
                            return RedirectToAction("Home", "AdminHome");
                        }
                        //scm
                        else
                        {
                            SCM scm = DBConnection.GetSCM(user_Id.Item1);
                            if (scm.PhotoBytes.Count() == 0)
                            {
                                scm.Photo = scm.DefaultPhoto();
                            }
                            else
                            {
                                scm.RenderImage();
                            }
                            TempData["scm"] = scm;
                            return RedirectToAction("Home", "SCMHome");
                        }
                    }
                    message = "Verifique los datos ingresados son incorrectos";
                    break;

                case 0:
                    message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
                    break;
            }
            return LogIn(message);
        }



    }
}