using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.DaL;
using DataLogicLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;
using System.Configuration;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace S2_mvc.Controllers
{
    public class HomeController : Controller
    {
        IBlogRepository repository = new BlogRepository();
       

        ICategoryRepository categorieRepository = new CategorieRepository();
        


        //Get Blogs admin page
        public IActionResult Blogs()
        {
            CategorieService categorieService = new CategorieService(categorieRepository);
            BlogService blogService = new BlogService(repository);
            List<Blog> blogs = blogService.GetBlogs();

            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;


            

            return View(blogViewModel);
        }


        //Get blogs
        public IActionResult UserBlogs(BlogDTO blogDto, int userId)
        {
            CategorieService categorieService = new CategorieService(categorieRepository);
            Blog blog = new Blog(blogDto);

            BlogService blogService = new BlogService(repository);
            List<Blog> blogs = blogService.GetBlogs();
            BlogViewModel blogViewModel = new BlogViewModel();
            blogViewModel.BlogList = blogs;

            blogViewModel.OwnerList = new List<Blog>();


            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;

            return View(blogViewModel);
        }


        //Get index page
        public IActionResult Index()
        {
            CategorieService categorieService = new CategorieService(categorieRepository);

            CategorieViewModel categorieViewModel = new CategorieViewModel();

            return View(categorieViewModel);
        }


        //Create blog
        [HttpPost]
        public IActionResult CreateBlog(BlogDTO blogDto, int id)
        {
            BlogService blogService = new BlogService(repository);

            blogService.CreateBlog(blogDto, id);

            return RedirectToAction("Blogs");
        }

        //Delete blog
        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            BlogService blogService = new BlogService(repository);
            blogService.DeleteBlog(id);

            return RedirectToAction("Blogs");
        }

        //Get selected blog for edit
        public IActionResult EditBlog(int id)
        {
            CategorieService categorieService = new CategorieService(categorieRepository);
            BlogService blogService = new BlogService(repository);

            EditBlogViewModel editBlogViewModel = new EditBlogViewModel();

            editBlogViewModel.blog = blogService.GetBlogById(id);

            editBlogViewModel.categories = categorieService.SetList();

            return View(editBlogViewModel);
        }

        //Save edits on selected blog
        public IActionResult SaveEditBlog(BlogDTO blogDto)
        {
            BlogService blogService = new BlogService(repository);

            blogService.EditBlog(blogDto);
            return RedirectToAction("EditBlog");
        }


        [HttpPost]
        public IActionResult CreateBlogUser(BlogDTO blogDto, int id)
        {
            BlogService blogService = new BlogService(repository);
            blogService.CreateBlog(blogDto, id);
            return RedirectToAction("UserBlogs");
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
