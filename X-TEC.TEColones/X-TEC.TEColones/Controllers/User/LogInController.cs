using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Models.SCMModels;
using X_TEC.TEColones.Persistence;
using System.Web.Security;

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
            FormsAuthentication.SignOut();
            TempData.Clear();
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
        /// Get View Change Password
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            Tuple<int, bool> user = (Tuple<int, bool>)TempData["user"];
            ViewBag.User = user;
            return View();
        }
        

        /// <summary>
        /// Action Update Password
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword()
        {
            Tuple<int, bool> user = (Tuple<int, bool>)TempData["user"];
            string password = Request["NewPasswordUser"].ToString();

            if (DBConnection.UpdatePasswordAdminSCM(user.Item1, password))
            {
                return LogInAdminSCM(user.Item1, user.Item2);
            }
            ViewBag.Message = "Ha ocurrido un error, vuelva a interarlo por favor";
            TempData["user"] = user;
            return RedirectToAction("ChangePassword", "LogIn");
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
                    message = "Verifique que los datos ingresados son incorrectos";
                    break;

                case 2:
                    var user_Id = DBConnection.VerifyAdminSCM(user, password);
                    if (user.Equals(password) && user_Id.Item1 != 0){
                        TempData["user"] = user_Id;
                        return RedirectToAction("ChangePassword", "LogIn");
                    }
                    if (user_Id.Item1 != 0)
                    {
                        return LogInAdminSCM(user_Id.Item1, user_Id.Item2);
                    }
                    message = "Verifique los datos ingresados son incorrectos";
                    break;

                case 0:
                    message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
                    break;
            }
            return LogIn(message);
        }


        /// <summary>
        /// Login for user admin or scm
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        protected ActionResult LogInAdminSCM(int idUser, bool isAdmin)
        {
            //admin
            if (isAdmin)
            {
                AdminModel adminModel = DBConnection.GetAdmin(idUser);
                if (adminModel.PhotoBytes.Count() == 0)
                {
                    adminModel.Photo = adminModel.DefaultPhoto();
                }
                else
                {
                    adminModel.RenderImage();
                }

                TempData["admin"] = adminModel;
                return RedirectToAction("Home", "AdminHome");
            }
            //scm
            else
            {
                SCM scm = DBConnection.GetSCM(idUser);
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

    }


}