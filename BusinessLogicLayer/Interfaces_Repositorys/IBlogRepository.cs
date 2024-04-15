using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBlogRepository
    {
            List<BlogDTO> GetBlogs();
            void CreateBlog(BlogDTO blogDto, int id);
            BlogDTO GetBlogById(int id);
            void DeleteBlogs(int id);
            void EditBlog(BlogDTO blogDto);
            List<BlogDTO> SearchBlogsByInput(string input);


    }
}
