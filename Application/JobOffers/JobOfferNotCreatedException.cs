using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    public class JobOfferNotCreatedException : BaseApplicationException
    {
        private const string BasicExceptionMessage = "JobOffer couldn't get created";
        public JobOfferNotCreatedException(string message) : base(message)
        {
        }

        public JobOfferNotCreatedException(string message, Exception? innerException) : base(message, innerException)
        {
        }

        public JobOfferNotCreatedException() : base(BasicExceptionMessage)
        {
        }
    }
}
