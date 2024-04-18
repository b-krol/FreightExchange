using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public class CartageOfferNotFoundException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageOffer not found";
        public CartageOfferNotFoundException(string message) : base(message)
        {
        }

        public CartageOfferNotFoundException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageOfferNotFoundException() : base(BasicExceptionMessage)
        {
        }
    }
}
