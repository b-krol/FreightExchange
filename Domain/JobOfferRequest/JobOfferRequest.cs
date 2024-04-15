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

        public JobOfferRequest(JobOffer.JobOffer requestedJobOffer, User.User applicant, int requestedPrice)
        {
            ThrowIfRequestedPriceIsHigherThanMaximumPrice(requestedJobOffer, requestedPrice);

            Applicant = applicant;
            Price = requestedPrice;
        }

        private void ThrowIfRequestedPriceIsHigherThanMaximumPrice(JobOffer.JobOffer jobOffer, int price)
        {
            if (price > jobOffer.MaximumPrice) throw new ArgumentException();//TODO
        }

    }
}
