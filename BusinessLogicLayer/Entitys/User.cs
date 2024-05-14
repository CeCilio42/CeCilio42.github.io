using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entitys
{
    public class User
    {
        public User() { }
        public User(UserDTO userDto) 
        {
            Id = userDto.Id;
            Username = userDto.Username;
            Password = userDto.Password;
            role = userDto.Role;
            profile_picture = userDto.profile_picture;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set;}

        public string role { get; set;}

        public string profile_picture { get; set;}
    }
}
