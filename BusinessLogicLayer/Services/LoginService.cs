using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository loginRepository;


        public LoginService(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public (bool, int) Login(string username, string password)
        {
            return loginRepository.Login(username, password);
        }

        public bool CheckRole(string username, string password)
        {
            if(loginRepository.CheckRole(username, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}
