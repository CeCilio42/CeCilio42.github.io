using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using DataAccessLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.Entitys
{
    public class Blog
    {
        public Blog() { }
        public Blog(BlogDTO blogDto)
        {
            Id = blogDto.id;
            Title = blogDto.Name;
            Text = blogDto.Description;
            categorieTitle = blogDto.CategoryName;
            Date = blogDto.Date;
            Username = blogDto.Username;
            ProfilePicture = blogDto.ProfilePicture;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string Date {  get; set; }
        public string categorieTitle {  get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }

        public User user { get; set; }

        public Categorie categorie { get; set; }


    }
}
