﻿using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class BlogService : IBlogService
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
        public BlogDTO CreateBlog(Blog blog, int id, int? user_id)
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
                BlogDTO blogDto = new BlogDTO(blog);
                repository.CreateBlog(blogDto, id, user_id);

                return blogDto;
            }

        }


        public List<Blog> SearchBlogsByInput(string input)
        {
            List<BlogDTO> blogDtos = repository.SearchBlogsByInput(input);
            List<Blog> blogs = blogDtos.Select(dto =>new Blog(dto)).ToList();
            return blogs;
            
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
        public void EditBlog(Blog blog)
        {
            BlogDTO blogDto = new BlogDTO(blog);
            repository.EditBlog(blogDto);
        }

        public List<Blog> GetUserBlogs(int? id)
        {
            List<BlogDTO> blogDtos = repository.GetUserBlogs(id);
            List<Blog> blogs = blogDtos.Select(dto => new Blog(dto)).ToList();
            return blogs;

        }

    }
}
