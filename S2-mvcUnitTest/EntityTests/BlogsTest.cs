using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using DataLogicLayer.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.EntityTests
{
    [TestClass]
    public class BlogsTest
    {
        BlogService blogService = new BlogService(new FakeDaL.FakeBlogRepo());

        [TestMethod]
        public void Blog_GetAllBlogs_BlogListIsNotNull()
        {
            List<Blog> blogs = blogService.GetBlogs();

            Assert.IsNotNull(blogs);
        }


        [TestMethod]
        public void Blog_SearchBlogsByInput_CountIsOne()
        {
            List<Blog> blogs = blogService.SearchBlogsByInput("School");

            Assert.AreEqual(1, blogs.Count());
        }

        [TestMethod]
        public void Blog_CreateBlog_IsTrue()
        {
            Blog blog = new Blog()
            {
                Id = 1,
                Title = "Title",
                Text = "Text",
            };

            //var test = blogService.CreateBlog(blog, 1, 1);

            //Assert.IsTrue();
        }
    }
}
