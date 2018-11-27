using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;
using X_TEC.TEColones.Models.SCMModels;
using X_TEC.TEColones.Persistence;

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
        public ActionResult LogInNewPassword()
        {

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
                                Id = user_Id.Item1,
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

                    return RedirectToAction("LogInNewPassword", "LogIn");
                }
                message = "Verifique los datos ingresados son incorrectos";
            }
            else
            {
                message = "El numero de usuario que ingresaste no coincide con ninguna cuenta. Regístrate para crear una cuenta.";
            }
            return ForgotPassword(message);
            

            //return RedirectToAction("LogInNewPassword", "LogIn");
        }

    }
}