using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartageOffer
{
    public class CartageOffer
    {
        public int Id {  get; set; }
        public User.User Applicant {  get; set; }
        public int Price { get; set; }
        public CartageOfferConsiderationStatus ConsiderationStatus { get; set; }

        public CartageOffer(CartageErrand.CartageErrand requestedCartageErrand, User.User applicant, int requestedPrice, CartageOfferConsiderationStatus considerationStatus)
        {
            ThrowIfRequestedPriceIsHigherThanMaximumPrice(requestedCartageErrand, requestedPrice);

            Applicant = applicant;
            Price = requestedPrice;
            ConsiderationStatus = considerationStatus;
        }

        private void ThrowIfRequestedPriceIsHigherThanMaximumPrice(CartageErrand.CartageErrand cartageErrand, int price)
        {
            if (price > cartageErrand.MaximumPrice) throw new ArgumentException();//TODO
        }

    }
}
