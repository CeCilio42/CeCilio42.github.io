using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Errors
{
    public class NoBlogsFoundException : Exception
    {
        public NoBlogsFoundException() { }
        public NoBlogsFoundException(string message) : base(message) { }
        public NoBlogsFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
