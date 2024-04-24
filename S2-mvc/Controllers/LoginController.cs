using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces_Services;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Session; 



namespace S2_mvc.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginService loginService;

        public LoginController(ILoginService _loginService)
        {
            loginService = _loginService;
        }

        //Login
        public IActionResult Login(string username, string password)
        {
            var (loginSuccessful, userId) = loginService.Login(username, password);

            if (loginSuccessful)
            {
                HttpContext.Session.SetInt32("User_ID", userId);

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

        public IActionResult Index()
        {
            return View();
        }
    }
}
