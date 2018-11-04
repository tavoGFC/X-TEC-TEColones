using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Student
{
    public class CreateAccountController : Controller
    {
        // GET: CreateAccount
        public ActionResult CreateAccount()
        {
            StudentModel student = new StudentModel();
            student.Photo = "";
            return View("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }

        [HttpPost]
        public ActionResult SignUp(StudentModel student)
        {
            Console.WriteLine("Nombre: " + student.FirstName);
            Console.WriteLine("Apellido: "+ student.LastName);
            Console.WriteLine("Carnet: " + student.Identification);
            Console.WriteLine("Correo: " +student.Email);
            Console.WriteLine("Descripcion: " + student.Description);
            Console.WriteLine("Habilidades: "  + student.Skills);
            Console.WriteLine("Universidad: " + student.University);
            Console.WriteLine("Sede: " + student.Headquarter);
            Console.WriteLine("Telefono: " +student.PhoneNumber);
            Console.WriteLine("Contraseña: " + student.Password);
            Console.WriteLine("**Archivo Foto: " + student.PhotoFile.FileName);

            student.Photo = "https://i.ytimg.com/vi/9kq6gHEO5Mo/maxresdefault.jpg";
            

            return View("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }

        
    }
}