using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using X_TEC.TEColones.Persistence;

namespace X_TEC.TEColones.Tests.DataBase
{
    [TestClass]
    public class UnitTestExistUser
    {
        [TestMethod]
        public void ExistUser_Student()
        {
            string id = "2014035394";
            int returnValue =  DBConnection.ExistUser(id);

            Assert.AreEqual(1, returnValue);
        }

        [TestMethod]
        public void ExistUser_Admin_SCM()
        {
            string id = "2014040801"; //scm
            //string idA = "2014047395"; //admin
            int returnValue = DBConnection.ExistUser(id);

            Assert.AreEqual(2, returnValue);
        }


        [TestMethod]
        public void ExistUser_Error()
        {
            string id = "20140000000";
            int returnValue = DBConnection.ExistUser(id);

            Assert.AreEqual(0, returnValue);
        }
    }
}
