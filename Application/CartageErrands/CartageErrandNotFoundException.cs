using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    public class CartageErrandNotFoundException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageErrand not found";
        public CartageErrandNotFoundException(string message) : base(message)
        {
        }

        public CartageErrandNotFoundException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandNotFoundException() : base(BasicExceptionMessage)
        {
        }
    }
}
