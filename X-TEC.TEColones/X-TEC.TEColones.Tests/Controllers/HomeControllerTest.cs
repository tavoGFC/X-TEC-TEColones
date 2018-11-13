using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using X_TEC.TEColones;
using X_TEC.TEColones.Controllers;

namespace X_TEC.TEColones.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void LogIn()
        {
            // Arrange
            LogInController controller = new LogInController();

            // Act
            ViewResult result = controller.LogIn() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {

            // Arrange
            LogInController controller = new LogInController();

            // Act
           

            // Assert

            
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            LogInController controller = new LogInController();

            // Act
        }
    }
}
