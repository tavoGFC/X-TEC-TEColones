using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEColones;
using TEColones.Controllers;

namespace TEColones.Tests.Controllers
{
    [TestClass]
    public class LogInControllerTest
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
        
    }
}
