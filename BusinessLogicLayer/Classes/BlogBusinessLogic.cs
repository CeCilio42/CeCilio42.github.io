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

        private BlogDaL database = new BlogDaL();


        public List<Blog> SetBlogs()
        {
            blogs = database.SetBlog();
            return blogs;
        }
        


    }
}
