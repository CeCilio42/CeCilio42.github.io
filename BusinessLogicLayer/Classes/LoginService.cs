using DataAccessLayer.DaL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class LoginService
    {
        LoginRepository LoginRepository { get; set; }
        BlogRepository BlogRepository { get; set; }

        public bool boolIsBlogOwnedByUser(int blogid, int userid)
        {
            return BlogRepository.IsBlogOwnedByUser(blogid, userid);
        }

        public int GetUserId(string username)
        {
            return LoginRepository.CheckId(username);
        }

       
    }
}
