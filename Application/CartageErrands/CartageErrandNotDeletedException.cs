using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    public class CartageErrandNotDeletedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageErrand couldn't get deleted";
        public CartageErrandNotDeletedException(string message) : base(message)
        {
        }

        public CartageErrandNotDeletedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandNotDeletedException() : base(BasicExceptionMessage)
        {
        }
    }
}
