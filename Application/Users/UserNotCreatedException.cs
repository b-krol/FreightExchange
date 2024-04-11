using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserNotCreatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "User couldn't get created";
        public UserNotCreatedException(string message) : base(message)
        {
        }

        public UserNotCreatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public UserNotCreatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
