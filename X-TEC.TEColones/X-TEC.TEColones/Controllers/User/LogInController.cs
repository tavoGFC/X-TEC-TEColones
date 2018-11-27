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
using System.Net.Mail;
using System.Configuration;
using System.Text;


namespace X_TEC.TEColones.Controllers
{
    public class LogInController : Controller
    {

        public void SendEmail(string emailTo, string result)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                string pwd = ConfigurationManager.AppSettings["password"].ToString();
                string email = ConfigurationManager.AppSettings["email"].ToString();
                string subjet = ConfigurationManager.AppSettings["subjetEmail"].ToString();
                string message = ConfigurationManager.AppSettings["messageEmail"].ToString();

                mail.From = new MailAddress(email);
                mail.To.Add(emailTo);
                mail.Subject = subjet;
                mail.Body = message + result;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, pwd);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error send email " + ex.Message);
            }
        }

        public string GeneratePassword()
        {
            int longitud = 10;
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

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
        public ActionResult ForgotPassword(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        /// <summary>
        /// Return Page LogInNewPassword
        /// </summary>
        /// <returns></returns>
        public ActionResult LogInNewPassword(string message)
        {
            ViewBag.Message = message;
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

            if (DBConnection.UpdatePassword(user.Item1, password))
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

        /// <summary>
        /// Get Page Log In New Password - Verify User in DB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword()
        {
            string user = Request["IdUser"].ToString();
            string email = Request["EmailUser"].ToString();

            int typeUser = DBConnection.ExistUser(user);
            string message = string.Empty;

            if (typeUser != 0)
            {
                string DBemail = DBConnection.VerifyEmail(user, email);
                if (DBemail != "0")
                {
                    string newPassword = GeneratePassword();
                    DBConnection.UpdatePassword(int.Parse(user), newPassword);
                    
                    SendEmail(DBemail,"Su contraseña temporal para el sistema TEColones es: " + newPassword);

                    TempData["usrIn"] = user;
                    return RedirectToAction("LogInNewPassword", "LogIn");
                }
                message = "Verifique los datos ingresados son incorrectos";
            }
            else
            {
                message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
            }
            return ForgotPassword(message);
      
        }

        /// <summary>
        /// Return Page LogInNewPassword
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogInNewPassword()
        {
            string tempPswrd = Request["IdTempPswrd"].ToString();
            string newPswrd = Request["IdNewPswrd"].ToString();
            string confirmPswrd = Request["IdConfirmPswrd"].ToString();

            string user = TempData["usrIn"].ToString();

            string message = string.Empty;

            if ( !newPswrd.Equals(confirmPswrd) )
            {
                message = "Error las contraseñas no coinciden, intente de nuevo";
                return LogInNewPassword(message);
            }

            else
            {
                
                int typeUser = DBConnection.ExistUser(user);

                switch (typeUser)
                {
                    case 1:
                        StudentModel student = DBConnection.VerifyStudent(user, tempPswrd);
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
                            DBConnection.UpdatePassword(student.Id,confirmPswrd);
                            return RedirectToAction("Home", "Home");
                        }
                        message = "Verifique que los datos ingresados son incorrectos";
                        break;

                    case 2:
                        var user_Id = DBConnection.VerifyAdminSCM(user, tempPswrd);
                        
                        if (user_Id.Item1 != 0)
                        {
                            DBConnection.UpdatePassword(user_Id.Item1, confirmPswrd);
                            return LogInAdminSCM(user_Id.Item1, user_Id.Item2);
                        }
                        message = "Verifique los datos ingresados son incorrectos";
                        break;

                    case 0:
                        message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
                        break;
                }
                return LogInNewPassword(message);
            }
            
        }

    }


}