using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BaseApplicationException : Exception
    {
        private const string BasicExceptionMessage = "Application exception";
        public BaseApplicationException(string message) : base(message)
        {
        }

        public BaseApplicationException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public BaseApplicationException() : base(BasicExceptionMessage) 
        {
        }

    }
}
