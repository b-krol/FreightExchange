using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.JobOfferRequest
{
    public class JobOfferRequest
    {
        public int Id {  get; set; }
        public User.User Applicant {  get; set; }
        public int Price { get; set; }

        public JobOfferRequest(CartageErrand.CartageErrand requestedCartageErrand, User.User applicant, int requestedPrice)
        {
            ThrowIfRequestedPriceIsHigherThanMaximumPrice(requestedCartageErrand, requestedPrice);

            Applicant = applicant;
            Price = requestedPrice;
        }

        private void ThrowIfRequestedPriceIsHigherThanMaximumPrice(CartageErrand.CartageErrand cartageErrand, int price)
        {
            if (price > cartageErrand.MaximumPrice) throw new ArgumentException();//TODO
        }

    }
}
