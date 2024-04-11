using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
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
        List<BlogDTO> blogs = new List<BlogDTO>();

        private readonly IBlogRepository repository;

        public BlogService(IBlogRepository repo)
        {
            repository = repo;
        }

        //Get All BlogsDTOs
        public List<Blog> GetBlogs()
        {
            var blogDtos = repository.GetBlogs();
            var blogs = blogDtos.Select(dto => new Blog(dto)).ToList();
            return blogs;
        }

        //Create Blog
        public BlogDTO CreateBlog(BlogDTO blogDTO, int id)
        {
            repository.CreateBlog(blogDTO, id);

            return blogDTO;
        }

        //Delete Blog
        public void DeleteBlog(int blogId)
        {
            repository.DeleteBlogs(blogId);
        }

        //Get selected blog for edit
        public Blog GetBlogById(int id)
        {
            BlogDTO blogDto = repository.GetBlogById(id);
            return new Blog(blogDto);
        }



        //Edit blog and save
        public void EditBlog(BlogDTO blogDto)
        {
            repository.EditBlog(blogDto);
        }

    }
}
