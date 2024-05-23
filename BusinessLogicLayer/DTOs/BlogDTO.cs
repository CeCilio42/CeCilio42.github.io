using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entitys;
using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO_s
{
    public class BlogDTO
    {
        public BlogDTO()
        {
            user = new UserDTO();
            categorie = new CategorieDTO();
        }

        public BlogDTO(Blog blog) : this()
        {
            id = blog.Id;
            Name = blog.Title;
            Description = blog.Text;
            categorie.Name = blog.categorie?.Title;
            Date = blog.Date;
            user.Username = blog.user?.Username;
            user.profile_picture = blog.user?.profile_picture;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategorieDTO categorie { get; set; }
        public UserDTO user { get; set; }

        public string Date {  get; set; }

    }
}
