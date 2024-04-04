using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataLogicLayer;
using DataLogicLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using S2_mvc.Models;
using System.Configuration;
using System.Diagnostics;
using System.Reflection.Metadata;


namespace S2_mvc.Controllers
{
    public class HomeController : Controller
    {
        DatabaseConnection connection = new DatabaseConnection();
        BlogService blogService = new BlogService();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }




        //Get Blogs
        public IActionResult Blogs()
        {
            BlogService blogBusinessLogic = new BlogService();
            List<Blog> blogs = blogBusinessLogic.GetBlogs();

            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            return View(blogViewModel);
        }

        //Get index page
        public IActionResult Index()
        {
            CategorieService categorieBusinessLogic = new CategorieService();
            List<Categorie> categories = categorieBusinessLogic.SetList();

            CategorieViewModel categorieViewModel = new CategorieViewModel();
            categorieViewModel.Categories = categories;

            return View(categorieViewModel);
        }


        //Create blog
        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            blogService.CreateBlog(blog);

            return RedirectToAction("Blogs");
        }


        

        // Your existing HttpPost method
        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            blogService.DeleteBlog(id);

            return RedirectToAction("Blogs");
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
