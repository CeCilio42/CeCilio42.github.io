using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.FakeDaL
{
    public class FakeBlogRepo : IBlogRepository
    {
        private List<BlogDTO> blogs;

        public FakeBlogRepo()
        {
            blogs = new List<BlogDTO>();

            var categorie1 = new CategorieDTO { Id = 1, Name = "Cat1" };
            var categorie2 = new CategorieDTO { Id = 2, Name = "Cat2" };

            var user1 = new UserDTO { Id = 1, Username = "user1", Password = "password1", profile_picture = "picture1", Role = "user" };
            var user2 = new UserDTO { Id = 2, Username = "user2", Password = "password2", profile_picture = "picture2", Role = "user" };


            blogs.Add(new BlogDTO { id = 1, Name = "Blog1", Description = "Desc1", categorie = categorie1, Date = "Today", user = user1 });
            blogs.Add(new BlogDTO { id = 2, Name = "Blog2", Description = "Desc2", categorie = categorie2, Date = "Today", user = user2 });


        }

        public List<BlogDTO> SearchBlogsByInput(string input)
        {
            List<BlogDTO> result = new List<BlogDTO>();

            foreach (var blog in blogs)
            {
                if (blog.Name.Contains(input) || blog.Description.Contains(input))
                {
                    result.Add(blog);
                }
            }

            return result;
        }

        public List<BlogDTO> GetUserBlogs(int? userId)
        {
            List<BlogDTO> result = new List<BlogDTO>();

            foreach (var blog in blogs)
            {
                if (blog.user.Id == userId)
                {
                    result.Add(blog);
                }
            }

            return result;
        }

        public List<BlogDTO> GetBlogs()
        {
            List<BlogDTO> result = new List<BlogDTO>();

            foreach (var blog in blogs)
            {
                result.Add(blog);
            }

            return result;
        }

        public BlogDTO GetBlogById(int id)
        {
            BlogDTO result = null;

            foreach (var blog in blogs)
            {
                if (blog.id == id)
                {
                    result = blog;
                    break;
                }
            }

            return result;
        }

        public void EditBlog(BlogDTO blogDTO)
        {
            foreach (var blog in blogs)
            {
                if (blog.id == blogDTO.id)
                {
                    blog.Name = blogDTO.Name;
                    blog.Description = blogDTO.Description;
                    blog.categorie = blogDTO.categorie;
                    blog.Date = blogDTO.Date;
                    blog.user = blogDTO.user;
                    break;
                }
            }
        }

        public void DeleteBlogs(int id)
        {
            BlogDTO blogToDelete = null;

            foreach (var blog in blogs)
            {
                if (blog.id == id)
                {
                    blogToDelete = blog;
                }
            }

            if (blogToDelete != null)
            {
                blogs.Remove(blogToDelete);
            }
        }

        public void CreateBlog(BlogDTO blogDTO, int categoryId, int? userId)
        {
            int newId = 0;

            foreach (var blog in blogs)
            {
                if (blog.id > newId)
                {
                    newId = blog.id;
                }
            }

            blogDTO.id = newId + 1;
            blogs.Add(blogDTO);
        }

    }

}
