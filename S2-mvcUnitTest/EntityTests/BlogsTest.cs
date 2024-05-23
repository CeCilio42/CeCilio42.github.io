using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTO_s;
using DataAccessLayer.Entitys;
using DataLogicLayer.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S2_mvcUnitTest.FakeDaL;
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
        [TestMethod]
        public void SearchBlogsByInput_ShouldReturnEmptyList()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            string input = "forloop";

            // Act
            List<Blog> result = blogService.SearchBlogsByInput(input);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetUserBlogs_ShouldReturn1Blog()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            int? userId = 1;

            // Act
            List<Blog> result = blogService.GetUserBlogs(userId);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetBlogs_ShouldReturn2Blogs()
        {
            BlogService blogService = new BlogService(new FakeBlogRepo());

            // Act
            List<Blog> result = blogService.GetBlogs();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetBlogById_ShouldReturnDefaultBlog()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            int blogId = 1;

            // Act
            Blog result = blogService.GetBlogById(blogId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Blog));
        }

        [TestMethod]
        public void EditBlog_ShouldNotThrowException()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            Blog blog = new Blog
            {
                Id = 1,
                Title = "Title",
                Text = "Text",
                Date = "Date",
            };

            // Act & Assert
            blogService.EditBlog(blog);
        }

        [TestMethod]
        public void DeleteBlog_ShouldNotThrowException()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            int blogId = 1;

            // Act & Assert
            blogService.DeleteBlog(blogId);
        }

        [TestMethod]
        public void CreateBlog_ShouldNotThrowException()
        {
            // Arrange
            BlogService blogService = new BlogService(new FakeBlogRepo());
            Blog blog = new Blog
            {
                Id = 1,
                Title = "Title",
                Text = "Text",
                Date = "Date",
            };
            int categoryId = 1;
            int? userId = 1;

            // Act & Assert
            blogService.CreateBlog(blog, categoryId, userId);
        }
    }

}

