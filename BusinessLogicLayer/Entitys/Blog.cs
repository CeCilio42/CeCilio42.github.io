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
        public Blog()
        {
            user = new User();
            categorie = new Categorie();
        }

        public Blog(BlogDTO blogDto) : this()
        {
            Id = blogDto.id;
            Title = blogDto.Name;
            Text = blogDto.Description;
            categorie.Title = blogDto.categorie?.Name;
            Date = blogDto.Date;
            user.Username = blogDto.user?.Username;
            user.profile_picture = blogDto.user?.profile_picture;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string Date {  get; set; }

        public User user { get; set; }

        public Categorie categorie { get; set; }


    }
}
