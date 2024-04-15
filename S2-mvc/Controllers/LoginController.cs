using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces_Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login(string username, string password, int id)
        {
            var (loginSuccessful, userId) = loginService.Login(username, password, id);

            bool admin = loginService.CheckRole(username, password);
            if (loginSuccessful && admin)
            {
                return RedirectToAction("Blogs", "Home");
            }
            else if (loginSuccessful)
            {
                return RedirectToAction("UserBlogs", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
