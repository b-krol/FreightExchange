using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    public class JobOfferNotFoundException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "JobOffer not found";
        public JobOfferNotFoundException(string message) : base(message)
        {
        }

        public JobOfferNotFoundException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public JobOfferNotFoundException() : base(BasicExceptionMessage)
        {
        }
    }
}
