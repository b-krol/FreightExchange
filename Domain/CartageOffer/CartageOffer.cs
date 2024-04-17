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

        public CartageOffer(User.User applicant, int requestedPrice, CartageOfferConsiderationStatus considerationStatus)
        {
            ThrowIfLessThanZero(requestedPrice);

            Applicant = applicant;
            Price = requestedPrice;
            ConsiderationStatus = considerationStatus;
        }

        private void ThrowIfLessThanZero(int value)
        {
            if (value < 0) 
                throw new ArgumentException();
        }
    }
}
