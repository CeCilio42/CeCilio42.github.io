using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Errors
{
    public class EventHandler
    {
        private string exceptionMessage;
        private string ex;

        public EventHandler(string exeption = "Connection failed, please contact a administrator.") 
        { }

        

        public EventHandler(string exception = "Connection failed, please contact an administrator.", string ex = "")
        {

        }
    }
}
