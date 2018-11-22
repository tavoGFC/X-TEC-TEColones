using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.SCMModels;
using X_TEC.TEColones.Persistence;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;

namespace X_TEC.TEColones.Controllers.StorageCenterManager
{
    public class SCMHomeController : Controller
    {
        

        // GET: SCMHome
        public ActionResult Home()
        {
            SCM scm = (SCM)TempData["scm"];
            DBConnection.GetMaterial(scm);
            return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
        }

        [HttpPost]
        public ActionResult Register() //(SCMModel emple) 
        {
            SCM scm = (SCM)TempData["scm"];

            IDictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string material in scm.Materials)
            {
                dict.Add(material, Request[material].ToString());
            }
            string carnet = Request["InputCarnet"].ToString();
            
            if (DBConnection.ExistUser(carnet).Equals(1))
            {
                string email = DBConnection.GetEmailUser(carnet);
                string messageToSend = String.Format("El estudiante {0} ha ingresado ", carnet);

                foreach (KeyValuePair<string, string> item in dict)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        //if (DBConnection.VerifyPromotion()){} --> APLICAR PROMOCION

                        //DBConnection.InsertRegister(carnet, scm.Id, item.Key, item.Value);
                        messageToSend += String.Format("{0} kg del material {1}. ", item.Value, item.Key);
                    }
                }

                //TwitterConnection.Publish(messageToTwitter); --> PUBLICAR MENSAJE A TWITTER

                SendEmail(email, messageToSend);

                ViewBag.Message = String.Format("Se registro el material con exito, para el estudiante {0}", carnet);
                return PartialView("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
            }
            else
            {
                ViewBag.Message = String.Format("Numero {0} no existe como Usuario Estudiante", carnet);
                return PartialView("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml", scm);
            }
        }

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
    }
}