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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public bool IsOwner { get; set; }

        public string CategoryTitle { get; set; }
        public User user { get; set; }

        

    }
}
