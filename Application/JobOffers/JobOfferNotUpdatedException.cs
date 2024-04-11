using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    public class JobOfferNotUpdatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "JobOffer couldn't get updated";
        public JobOfferNotUpdatedException(string message) : base(message)
        {
        }

        public JobOfferNotUpdatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public JobOfferNotUpdatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
