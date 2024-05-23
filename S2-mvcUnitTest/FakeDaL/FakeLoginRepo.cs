using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.FakeDaL
{
    public class FakeLoginRepo : ILoginRepository
    {
        private List<User> users;

        public FakeLoginRepo()
        {
            users = new List<User>();


            users.Add(new User { Id = 1, Username = "admin", Password = "password", role = "admin", profile_picture = "123" });
            users.Add(new User { Id = 2, Username = "user", Password = "password", role = "user", profile_picture = "123" });
            
        }

        public (bool, int, string) Login(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return (true, user.Id, user.role);
                }
            }
            return (false, 0, null);
        }

        public bool CheckRole(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return user.role == "admin";
                }
            }
            return false;
        }
    }

}
