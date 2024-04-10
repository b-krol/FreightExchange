using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserNotFoundException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "User not found";
        public UserNotFoundException(string message) : base(message) 
        {
        }

        public UserNotFoundException(string message, Exception? innerException) : base(message, innerException) 
        {
        }

        public UserNotFoundException() : base(BasicExceptionMessage) 
        {
        }
    }
}
