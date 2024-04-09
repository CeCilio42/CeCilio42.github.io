using BusinessLogicLayer.Entitys;
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
        public Blog CreateBlog(Blog blog, int categoryId)
        {
            repository.CreateBlog(blog, categoryId);
            
            return blog;
        }

        //Delete Blog
        public void DeleteBlog(int blogId) 
        {
            repository.DeleteBlogs(blogId);  
        }

        //Get selected blog for edit
        public Blog ShowSelectedBlogToEdit(int id)
        {
            return repository.GetSelectedBlogToEdit(id);

        }

        //Edit blog and save
        public void EditBlog(Blog blog)
        {
            repository.EditBlog(blog);
        }

        public bool IsBlogOwnedByUser(int blogid, int userid)
        {
            return repository.IsBlogOwnedByUser(blogid, userid);
        }
    }
}
