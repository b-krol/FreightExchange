using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartageErrand
{
    public class CartageErrandNewCartageOfferReceivingNotAllowedException : BaseDomainException
    {
        private const string BasicExceptionMessage = "Cannot receive new CartageOffer";
        public CartageErrandNewCartageOfferReceivingNotAllowedException(string message) : base(message)
        {
        }

        public CartageErrandNewCartageOfferReceivingNotAllowedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandNewCartageOfferReceivingNotAllowedException() : base(BasicExceptionMessage)
        {
        }
    }
}
