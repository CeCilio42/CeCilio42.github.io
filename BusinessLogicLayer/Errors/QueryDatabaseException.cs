using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Errors
{
    public class QueryDatabaseException : Exception
    {
        public QueryDatabaseException() { }
        public QueryDatabaseException(string message) : base(message) { }
        public QueryDatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
