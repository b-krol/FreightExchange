using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserNotDeletedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "User couldn't get deleted";
        public UserNotDeletedException(string message) : base(message)
        {
        }

        public UserNotDeletedException(string message, Exception? innerException) : base(message, innerException) 
        {
        }

        public UserNotDeletedException() : base(BasicExceptionMessage)
        {
        }
    }
}
