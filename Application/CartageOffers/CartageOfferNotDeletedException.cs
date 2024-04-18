using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public class CartageOfferNotDeletedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "CartageOffer couldn't get deleted";
        public CartageOfferNotDeletedException(string message) : base(message)
        {
        }

        public CartageOfferNotDeletedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public CartageOfferNotDeletedException() : base(BasicExceptionMessage)
        {
        }
    }
}
