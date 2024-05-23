using BusinessLogicLayer.Classes;
using DataAccessLayer.DaL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S2_mvcUnitTest.FakeDaL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.EntityTests
{
    [TestClass]
    public class LoginTest
    {
        private LoginService loginService;

        public LoginTest()
        {
            loginService = new LoginService(new FakeLoginRepo());
        }

        [TestMethod]
        public void LoggedIn_IsTrue()
        {
            // Arrange
            string username = "admin";
            string password = "password";

            // Act
            var (loggedIn, _, _) = loginService.Login(username, password);

            // Assert
            Assert.AreEqual(true, loggedIn);
        }

        [TestMethod]
        public void LoggedIn_IsFalse()
        {
            // Arrange
            string username = "cilio";
            string password = "password";

            // Act
            var (loggedIn, _, _) = loginService.Login(username, password);

            // Assert
            Assert.AreEqual(false, loggedIn);
        }

        [TestMethod]
        public void LoggedInUser_RoleAdmin_IsTrue()
        {
            // Arrange
            string username = "admin";
            string password = "password";

            // Act
            bool role = loginService.CheckRole(username, password);

            // Assert
            Assert.AreEqual(true, role);
        }

        [TestMethod]
        public void LoggedInUser_RoleAdmin_IsFalse()
        {
            // Arrange
            string username = "user";
            string password = "password";

            // Act
            bool role = loginService.CheckRole(username, password);

            // Assert
            Assert.AreEqual(false, role);
        }
    }

}

