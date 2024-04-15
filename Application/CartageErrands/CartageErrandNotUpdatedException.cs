using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    public class CartageErrandNotUpdatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageErrand couldn't get updated";
        public CartageErrandNotUpdatedException(string message) : base(message)
        {
        }

        public CartageErrandNotUpdatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandNotUpdatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
