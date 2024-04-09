using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataAccessLayer;
using DataAccessLayer.DaL;
using DataAccessLayer.Entitys;
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
        CategorieService categorieService = new CategorieService();
        LoginService loginService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }



        //Get Blogs admin page
        public IActionResult Blogs(Blog blog)
        {
            List<Blog> blogs = blogService.GetBlogs();
            
            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;


            return View(blogViewModel);
        }


        //Get blogs 
        public IActionResult UserBlogs(Blog blog, int userId)
        {
          
            List<Blog> blogs = blogService.GetBlogs();
            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            blogViewModel.OwnerList = new List<Blog>();


            CategorieService categorieService = new CategorieService();
            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;

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
        public IActionResult CreateBlog(Blog blog, int categoryID)
        {
            blogService.CreateBlog(blog, categoryID);

            return RedirectToAction("Blogs");
        }

        //Delete blog
        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            blogService.DeleteBlog(id);

            return RedirectToAction("Blogs");
        }

        //Get selected blog for edit
        public IActionResult EditBlog(int id)
        {
            EditBlogViewModel editBlogViewModel = new EditBlogViewModel();
            editBlogViewModel.blog = blogService.ShowSelectedBlogToEdit(id);

            editBlogViewModel.categories = categorieService.SetList();

            return View(editBlogViewModel);
        }

        //Save edits on selected blog
        public IActionResult SaveEditBlog(Blog blog)
        {

            blogService.EditBlog(blog);
            return RedirectToAction("EditBlog"); 
        }


        [HttpPost]
        public IActionResult CreateBlogUser(Blog blog, int categoryID)
        {
            blogService.CreateBlog(blog, categoryID);

            return RedirectToAction("UserBlogs");
        }

        //Get categories to edit
        public IActionResult EditCategories()
        {
            EditCategoriesViewModel editCategoriesViewModel = new EditCategoriesViewModel();
            CategorieService categorieService = new CategorieService();

            editCategoriesViewModel.categories = categorieService.SetList();

            return View(editCategoriesViewModel);
        }

        //Create category
        public IActionResult CreateCategorie(Categorie categorie) 
        {
            categorieService.CreateCategorie(categorie);

            return RedirectToAction("EditCategories");
        }

        //Save edit category
        public ActionResult updateCategorie(Categorie categorie)
        {
            categorieService.EditCategorie(categorie);
            return RedirectToAction("EditCategories");
        }



        //Login
        public IActionResult Login(string username, string password, int id)
        {
            LoginRepository loginRepository = new LoginRepository();
            var (loginSuccessful, userId) = loginRepository.Login(username, password, id);

            bool admin = loginRepository.CheckRole(username, password);
            if (loginSuccessful && admin) 
            {
                return RedirectToAction("Blogs");
            }
            else if (loginSuccessful)
            {
                return RedirectToAction("UserBlogs");
            }
            else 
            {
               return RedirectToAction("index");
            }
            
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
