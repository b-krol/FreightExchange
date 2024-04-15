using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    public class CartageErrandNotCreatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageErrand couldn't get created";
        public CartageErrandNotCreatedException(string message) : base(message)
        {
        }

        public CartageErrandNotCreatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandNotCreatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
