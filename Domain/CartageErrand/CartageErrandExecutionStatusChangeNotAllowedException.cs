using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartageErrand
{
    public class CartageErrandExecutionStatusChangeNotAllowedException : BaseDomainException
    {
        private const string BasicExceptionMessage = "Cannot change CartageErrand ExecutionStatus";
        public CartageErrandExecutionStatusChangeNotAllowedException(string message) : base(message)
        {
        }

        public CartageErrandExecutionStatusChangeNotAllowedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandExecutionStatusChangeNotAllowedException() : base(BasicExceptionMessage)
        {
        }
    }
}
