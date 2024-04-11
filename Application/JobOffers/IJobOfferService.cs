using Application.Users;
using Domain.JobOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    public interface IJobOfferService
    {
        IEnumerable<JobOfferDto> GetAll();
        JobOfferDto GetById(int id);
        void Delete(int id);
        int Create(JobOfferDto jobOfferDto);
        JobOfferDto Update(JobOfferDto jobOfferDto);
    }
}
