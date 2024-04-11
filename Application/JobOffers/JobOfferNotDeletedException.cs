using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    public class JobOfferNotDeletedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "JobOffer couldn't get deleted";
        public JobOfferNotDeletedException(string message) : base(message)
        {
        }

        public JobOfferNotDeletedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public JobOfferNotDeletedException() : base(BasicExceptionMessage)
        {
        }
    }
}
