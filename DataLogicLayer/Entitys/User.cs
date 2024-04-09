using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entitys
{
    public class User
    {
        public int Id { get; set; }
        public string User_Username { get; set; }
        public string User_Password { get; set;}
        
        //public bool role { get; set; }
    }
}
