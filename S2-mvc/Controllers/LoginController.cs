using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DaL;
using Microsoft.AspNetCore.Mvc;

namespace S2_mvc.Controllers
{
    public class LoginController : Controller
    {
        ILoginRepository _loginRepository = new LoginRepository();


        //Login
        public IActionResult Login(string username, string password, int id)
        {
            LoginService loginService = new LoginService(_loginRepository);
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
