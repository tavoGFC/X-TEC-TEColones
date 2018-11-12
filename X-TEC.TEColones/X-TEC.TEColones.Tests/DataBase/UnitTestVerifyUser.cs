using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using X_TEC.TEColones.Models;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Tests.DataBase
{
    [TestClass]
    public class UnitTestVerifyUser
    {
        [TestMethod]
        public void VerifyUser_Student_Exito()
        {
            string identification = "2014035394";
            string password = "gustavo123";

            StudentModel model = DBConnection.VerifyStudent(identification, password);
            Console.WriteLine("Bienvenido!!!: " + model.FirstName + "  " + model.LastName);

            Assert.AreEqual(1, model.Id);

        }


        [TestMethod]
        public void VerifyUser_Student_Error()
        {
            string identification = "2014035394";
            string password = "gustavo12345";

            StudentModel model = DBConnection.VerifyStudent(identification, password);
            Console.WriteLine("Password o Identificacion incorrecto!!");

            Assert.AreEqual(0, model.Id);

        }

    }
}
