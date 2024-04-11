using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserNotUpdatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "User couldn't get updated";
        public UserNotUpdatedException(string message) : base(message)
        {
        }

        public UserNotUpdatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public UserNotUpdatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
