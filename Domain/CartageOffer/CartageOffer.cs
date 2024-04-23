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
        public int Id { get; set; }
        internal int ErrandId { get; init; }
        public int BidderId { get; init; }
        public User.User Bidder {  get; set; }
        public int Price { get; set; }
        public CartageOfferConsiderationStatus ConsiderationStatus { get; set; }

        private CartageOffer()
        {
            //required by ORM
        }
        public CartageOffer(User.User bidder, int requestedPrice)
        {
            ThrowIfLessThanZero(requestedPrice);

            Bidder = bidder;
            Price = requestedPrice;
            ConsiderationStatus = CartageOfferConsiderationStatus.Waiting;
        }

        private void ThrowIfLessThanZero(int value)
        {
            if (value < 0) 
                throw new ArgumentException();
        }
    }
}
