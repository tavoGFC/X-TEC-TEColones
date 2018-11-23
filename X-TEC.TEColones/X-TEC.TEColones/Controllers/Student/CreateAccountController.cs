using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Controllers.Student
{
    public class CreateAccountController : Controller
    {
        // GET: CreateAccount
        public ActionResult CreateAccount()
        {
            CreateUser student = new CreateUser();
            return View("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }
        


        [HttpPost]
        public ActionResult SignUp(CreateUser student)
        {           
            //if (student.PhotoFile != null)
            //{
            //    Stream stream = student.PhotoFile.InputStream;
            //    BinaryReader br = new BinaryReader(stream);
            //    byte[] imgByte = br.ReadBytes((Int32)stream.Length);
            //    student.ImageByte = imgByte;
            //}
        {
           
            

            //if (DBConnection.InsertStudent(student))
            //{
            //    StudentModel studentModel = new StudentModel
            //    {
            //        Id = student.Id,
            //        FirstName = student.FirstName,
            //        LastName = student.LastName,
            //        University = student.University,
            //        Headquarter = student.Headquarter,
            //        PhotoBytes = student.ImageByte,
            //        Email = student.Email,
            //        Skills = student.Skills,
            //        Description = student.Description,
            //        PhoneNumber = student.PhoneNumber,
            //        TCS = 0
            //    };
            //    if (studentModel.PhotoBytes.Count() == 0)
            //    {
            //        studentModel.Photo = "http://cdn.onlinewebfonts.com/svg/img_569204.png";
            //    }
            //    else
            //    {
            //        studentModel.RenderImage();
            //    }
            //    DBConnection.GetBenefit(studentModel);
            //    DBConnection.GetMaterial(studentModel);
            //    
            //    return View("~/Views/Student/Home/Home.cshtml", studentModel);
            //}

            ViewBag.Message = "Existe un usuario con el mismo carnet, verifique por favor";
            return View("~/Views/Student/CreateAccount/CreateAccount.cshtml", student); //PartialView("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }
        
    }
}