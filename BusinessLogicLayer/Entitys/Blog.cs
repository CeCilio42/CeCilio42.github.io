using BusinessLogicLayer.DTO_s;
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
        public Blog(BlogDTO blogDto)
        {
            Id = blogDto.id;
            Title = blogDto.Name;
            Text = blogDto.Description;
            CategoryTitle = blogDto.CategoryTitle;

        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }

        public string CategoryTitle { get; private set; }
        public User user { get; private set; }

        

    }
}
