using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;
using System.Diagnostics;

namespace S2_mvc.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            BlogBusinessLogic blogBusinessLogic = new BlogBusinessLogic();
            List<Blog> blogs = blogBusinessLogic.SetBlogs();

            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            return View(blogViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
