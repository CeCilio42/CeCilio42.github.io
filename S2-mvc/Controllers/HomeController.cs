using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;
using System.Diagnostics;


namespace S2_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            return View(blog);
        }
        
        public IActionResult Blogs()
        {
            BlogBusinessLogic blogBusinessLogic = new BlogBusinessLogic();
            List<Blog> blogs = blogBusinessLogic.SetBlogs();

            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            return View(blogViewModel);
        }


        public IActionResult Index()
        {
            CategorieBusinessLogic categorieBusinessLogic = new CategorieBusinessLogic();
            List<Categorie> categories = categorieBusinessLogic.SetList();
            
            CategorieViewModel categorieViewModel = new CategorieViewModel();
            categorieViewModel.Categories = categories;

            return View(categorieViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

  
}
