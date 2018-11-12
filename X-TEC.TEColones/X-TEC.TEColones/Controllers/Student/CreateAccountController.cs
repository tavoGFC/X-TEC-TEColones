﻿using System;
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
        public ActionResult CreateAccount(string message, StudentModel student)
        {
            ViewBag.Message = message;
            return View("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }

        [HttpPost]
        public ActionResult SignUp(StudentModel student)
        {

            if (DBConnection.InsertStudent(student))
            {
                return View("~/Views/Student/Home/Home.cshtml", student);
            }
            
            return CreateAccount("Existe un usuario con el mismo carnet, verifique por favor", student); //PartialView("~/Views/Student/CreateAccount/CreateAccount.cshtml", student);
        }

        
    }
}