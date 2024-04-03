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
    public class BlogBusinessLogic
    {
        List<Blog> blogs = new List<Blog> ();

        private BlogRepository repository = new BlogRepository();



        //Get All Blogs
        public List<Blog> SetBlogs()
        {
            blogs = repository.GetBlogs();
            return blogs;
        }
        

        //Create New Blogs


    }
}
