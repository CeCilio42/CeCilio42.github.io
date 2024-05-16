using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Errors
{
    public class CustomUserFriendlyException : Exception
    {
        public CustomUserFriendlyException() { }
        public CustomUserFriendlyException(string message) : base(message) { }
        public CustomUserFriendlyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
