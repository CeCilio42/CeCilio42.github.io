using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Session;
using DataAccessLayer.DaL;



namespace S2_mvc.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginService loginService;

        public LoginController()
        {
            loginService = new LoginService(new LoginRepository());
        }


        //Login
        public IActionResult Login(string username, string password)
        {
            var (loginSuccessful, userId, profilePictureURL) = loginService.Login(username, password);

            if (loginSuccessful)
            {
                HttpContext.Session.SetInt32("User_ID", userId);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("ProfilePicURL", profilePictureURL);

                bool admin = loginService.CheckRole(username, password);
                if (admin)
                {
                    return RedirectToAction("Blogs", "Home");
                }
                else
                {
                    return RedirectToAction("UserBlogs", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Profile()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
