using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Errors
{
    public class ServiceResponce
    {
        public bool Success { get; set; } 
        public string ErrorMessage { get; set; }
    }
}
