using BusinessLogicLayer.Classes;
using DataAccessLayer.DaL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        LoginService loginService = new LoginService(new LoginRepository());


        [TestMethod]
        public void LoggedIn_IsTrue()
        {
            var (loggedIn, _, _) = loginService.Login("Jamal", "password");


            Assert.AreEqual(1, Convert.ToInt32(loggedIn));
        }

        [TestMethod]
        public void LoggedInUser_RoleAdmin_IsTrue()
        {
            bool role = loginService.CheckRole("admin_cilio", "password");

            Assert.AreEqual(role, true);
        }

        [TestMethod]
        public void LoggedInUser_RoleAdmin_IsFalse()
        {
            bool role = loginService.CheckRole("Jamal", "password");

            Assert.AreEqual(role, false);
        }
    }
}
