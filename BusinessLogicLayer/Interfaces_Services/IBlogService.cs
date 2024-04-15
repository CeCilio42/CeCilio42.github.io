using BusinessLogicLayer.DTO_s;
using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces_Services
{
    public interface IBlogService
    {
        List<Blog> GetBlogs();
        BlogDTO CreateBlog(Blog blog, int id);
        void DeleteBlog(int blogId);
        Blog GetBlogById(int id);
        void EditBlog(Blog blog);
        List<Blog> SearchBlogsByInput(string input);
    }
}
