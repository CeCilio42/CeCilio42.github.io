using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces_Services;
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
        private readonly IBlogService blogService;
        private readonly ICategorieService categorieService;

        public HomeController(IBlogService _blogservice, ICategorieService _categorieService)
        {
            blogService = _blogservice;
            categorieService = _categorieService;
        }

        BlogViewModel blogViewModel = new BlogViewModel();


        //Get Blogs admin page
        public IActionResult Blogs()
        {


            List<Blog> blogs = blogService.GetBlogs();

            blogViewModel.BlogList = blogs;

            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;


            return View(blogViewModel);
        }

        //SearchBlogs
        public IActionResult SearchBlogs(string input)
        {
            blogViewModel.SearchList = blogService.SearchBlogsByInput(input);


            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;

            return View(blogViewModel);
        }

        //Get blogs
        public IActionResult UserBlogs()
        {
            List<Blog> blogs = blogService.GetBlogs();

            blogViewModel.BlogList = blogs;


            List<Categorie> options = categorieService.SetList();
            blogViewModel.categories = options;

            return View(blogViewModel);
        }

        

        //Get index page
        public IActionResult Index()
        {

            CategorieViewModel categorieViewModel = new CategorieViewModel();

            ViewData["ShowSidebar"] = false;

            return View(categorieViewModel);
        }


        //Create blog
        [HttpPost]
        public IActionResult CreateBlog(Blog blog, int id)
        {
            blogService.CreateBlog(blog, id);

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

            editBlogViewModel.blog = blogService.GetBlogById(id);

            editBlogViewModel.categories = categorieService.SetList();

            return View(editBlogViewModel);
        }

        //Save edits on selected blog
        public IActionResult SaveEditBlog(Blog blog)
        {
            blogService.EditBlog(blog);
            return RedirectToAction("Blogs");
        }


        [HttpPost]
        public IActionResult CreateBlogUser(Blog blog, int id)
        {
            blogService.CreateBlog(blog, id);
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
