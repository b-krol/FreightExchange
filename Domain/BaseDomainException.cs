using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseDomainException : Exception
    {
        private const string BasicExceptionMessage = "Domain exception";
        public BaseDomainException(string message) : base(message)
        {
        }

        public BaseDomainException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public BaseDomainException() : base(BasicExceptionMessage)
        {
        }
    }
}
