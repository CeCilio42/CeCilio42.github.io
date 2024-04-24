using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ILoginRepository
    {
        (bool, int) Login(string username, string password);
        bool CheckRole(string username, string password);
    }
}
