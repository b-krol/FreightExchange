using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartageErrand
{
    public class CartageErrandAddingNewCartageOfferNotAcceptedException : BaseDomainException
    {
        private const string BasicExceptionMessage = "Cannot receive new CartageOffer";
        public CartageErrandAddingNewCartageOfferNotAcceptedException(string message) : base(message)
        {
        }

        public CartageErrandAddingNewCartageOfferNotAcceptedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageErrandAddingNewCartageOfferNotAcceptedException() : base(BasicExceptionMessage)
        {
        }
    }
}
