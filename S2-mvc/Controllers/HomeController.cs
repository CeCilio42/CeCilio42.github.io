using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Errors;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entitys;
using DataLogicLayer.DaL;
using DataLogicLayer.Entitys;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using S2_mvc.Models;
using System.Configuration;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace S2_mvc.Controllers
{
    public class HomeController : Controller
    {
        BlogViewModel blogViewModel = new BlogViewModel();

        private readonly BlogService blogService;
        private readonly CategorieService categorieService;

        public HomeController()
        {
            blogService = new BlogService(new BlogRepository()); 
            categorieService = new CategorieService(new CategorieRepository());
        }

        //Get Blogs admin page
        public IActionResult Blogs()
        {
            string profile_picture = HttpContext.Session.GetString("ProfilePicURL");
            blogViewModel.User = new User { profile_picture = profile_picture };
            try
            {
                List<Blog> blogs = blogService.GetBlogs();
                blogViewModel.BlogList = blogs;

                List<Categorie> options = categorieService.SetList();
                blogViewModel.categories = options;

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }



            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(blogViewModel);
        }

        //SearchBlogs
        public IActionResult SearchBlogs(string input)
        {
            string profile_picture = HttpContext.Session.GetString("ProfilePicURL");
            string username = HttpContext.Session.GetString("username");
            blogViewModel.User = new User { profile_picture = profile_picture, Username = username };

            try
            {
                blogViewModel.SearchList = blogService.SearchBlogsByInput(input);


                List<Categorie> options = categorieService.SetList();
                blogViewModel.categories = options;

                return View(blogViewModel);
            }
            catch(CustomUserFriendlyException ex)
            {
                blogViewModel.SearchList = blogService.SearchBlogsByInput(input);


                List<Categorie> options = categorieService.SetList();
                blogViewModel.categories = options;

                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }

                return View(blogViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }


        }

        //Get blogs
        public IActionResult UserBlogs(int? userId)
        {
            try
            {
                userId = HttpContext.Session.GetInt32("User_ID");
                string profile_picture = HttpContext.Session.GetString("ProfilePicURL");
                string username = HttpContext.Session.GetString("username");
                blogViewModel.User = new User { profile_picture = profile_picture, Username = username };


                List<Blog> OwnersList = blogService.GetUserBlogs(userId);

                blogViewModel.OwnersList = OwnersList;
                blogViewModel.BlogList = blogService.GetBlogs();

                blogViewModel.BlogList.RemoveAll(blog => OwnersList.Any(ownerblog => ownerblog.Id == blog.Id));


                List<Categorie> options = categorieService.SetList();
                blogViewModel.categories = options;

                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                }

            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }

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
        public IActionResult CreateBlog(Blog blog, int id, int? user_id)
        {
            try
            {
                user_id = HttpContext.Session.GetInt32("User_ID");
                var response = blogService.CreateBlog(blog, id, user_id);
                if (!response.Success)
                {
                    TempData["ErrorMessage"] = response.ErrorMessage;
                }
                return RedirectToAction("Blogs");
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }
        }




        //Delete blog
        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                blogService.DeleteBlog(id);

                return RedirectToAction("Blogs");
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }
        }

        //Delete blog
        [HttpPost]
        public IActionResult DeleteBlogUser(int id)
        {
            try
            {
                blogService.DeleteBlog(id);

                return RedirectToAction("UserBlogs");
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }
        }

        //Get selected blog for edit
        public IActionResult EditBlog(int id)
        {
            try
            {
                EditBlogViewModel editBlogViewModel = new EditBlogViewModel();

                editBlogViewModel.blog = blogService.GetBlogById(id);

                editBlogViewModel.categories = categorieService.SetList();

                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                }

                return View(editBlogViewModel);
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }

        }

        //Save edits on selected blog
        public IActionResult SaveEditBlog(Blog blog)
        {
            try
            {
                var response = blogService.EditBlog(blog);
                if (!response.Success)
                {
                    TempData["ErrorMessage"] = response.ErrorMessage;
                    return RedirectToAction("UserBlogs");
                }
                return RedirectToAction("UserBlogs");
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }

        }


        [HttpPost]
        public IActionResult CreateBlogUser(Blog blog, int id, int? user_id)
        {
            try
            {
                user_id = HttpContext.Session.GetInt32("User_ID");

                var response = blogService.CreateBlog(blog, id, user_id);
                if (!response.Success)
                {
                    TempData["ErrorMessage"] = response.ErrorMessage;
                }
                return RedirectToAction("UserBlogs");
            }
            catch (CustomUserFriendlyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch { return StatusCode(500); }

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
