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
        public BlogDTO() { }

        public BlogDTO(Blog blog)
        {
            id = blog.Id;
            Name = blog.Title;
            Description = blog.Text;
            CategoryTitle = blog.CategoryTitle;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryTitle { get; set; }
    }
}
