using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Entitys;
using BusinessLogicLayer.Errors;
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
            List<BlogDTO> blogDtos = repository.GetBlogs();
            List<Blog> blogs = blogDtos.Select(dto => new Blog(dto)).ToList();
            return blogs;
        }

        //Create Blog
        public ServiceResponce CreateBlog(Blog blog, int id, int? user_id)
        {
            ServiceResponce response = new ServiceResponce() { Success = false };

            if (blog.Title.Length >= 100 && blog.Title.Length != 0)
            {
                response.ErrorMessage = "Title must be shorter than 100 characters";
                return response;
            }
            if (blog.Text.Length >= 1000 && blog.Text.Length != 0)
            {
                response.ErrorMessage = "Blog must be shorter than 1000 characters";
                return response;
            }

            try
            {
                    BlogDTO blogDto = new BlogDTO(blog);
                    repository.CreateBlog(blogDto, id, user_id);
                    response.Success = true;
                
            }
            catch (CustomUserFriendlyException ex)
            {
                response.Success = false;
                response.ErrorMessage = "A program error occurred. Please contact admin: " + ex.Message;
            }

            return response;
        }



        public List<Blog> SearchBlogsByInput(string input)
        {
            if (input.Length > 50)
            {
                throw new CustomUserFriendlyException("Search must be shorter than 50 characters");
            }

            try
            {
                List<BlogDTO> blogDtos = repository.SearchBlogsByInput(input);
                List<Blog> blogs = blogDtos.Select(dto => new Blog(dto)).ToList();
                return blogs;
            }
            catch (NoBlogsFoundException ex)
            {
                throw new CustomUserFriendlyException(ex.Message);
            }
            catch(Exception ex)
            {
                throw;
            }

            
        }


        //Delete Blog
        public void DeleteBlog(int blogId)
        {
            try
            {
                repository.DeleteBlogs(blogId);
            }
            catch (QueryDatabaseException ex)
            {
                throw new CustomUserFriendlyException("A program error occurred. Please contact admin" + ex.Message);
            }

        }

        //Get selected blog for edit
        public Blog GetBlogById(int id)
        {
            try
            {
                BlogDTO blogDto = repository.GetBlogById(id);
                return new Blog(blogDto);
            }
            catch(QueryDatabaseException ex)
            {
                throw new CustomUserFriendlyException ("A program error occurred. Please contact admin" + ex.Message);
            }

        }



        //Edit blog and save
        public ServiceResponce EditBlog(Blog blog)
        {
            ServiceResponce response = new ServiceResponce();


            try
            {
                if (blog.Title.Length >= 100 && blog.Title.Length != 0)
                {
                    throw new ArgumentException("Title must be shorter than 100 characters");
                }
                else if (blog.Text.Length >= 1000 && blog.Text.Length != 0)
                {
                    throw new ArgumentException("Blog must be shorter than 1000 characters");
                }
                else
                {
                    BlogDTO blogDTO = new BlogDTO(blog);
                    repository.EditBlog(blogDTO);
                    response.Success = true;

                }
            }
            catch (CustomUserFriendlyException argEx)
            {
                response.Success = false;
                response.ErrorMessage = "A program error occurred.Please contact admin" + argEx.Message;
            }

            return response;

        }

        public List<Blog> GetUserBlogs(int? id)
        {
            try
            {
                List<BlogDTO> blogDtos = repository.GetUserBlogs(id);
                List<Blog> blogs = blogDtos.Select(dto => new Blog(dto)).ToList();
                return blogs;
            }
            catch (QueryDatabaseException ex) 
            { 
                throw new CustomUserFriendlyException("A program error occurred. Please contact admin" + ex.Message);
            }




        }

    }
}
