using DataLogicLayer.DaL;
using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class BlogService
    {
        List<Blog> blogs = new List<Blog> ();

        private BlogRepository repository = new BlogRepository();



        //Get All Blogs
        public List<Blog> GetBlogs()
        {
            blogs = repository.GetBlogs();
            return blogs;
        }
        
        //Create Blog
        public Blog CreateBlog(Blog blog)
        {
            repository.CreateBlog(blog);

            return blog;
        }

        //Delete Blog
        public void DeleteBlog(int blogId) 
        {
            repository.DeleteBlogs(blogId);  
        }

    }
}
