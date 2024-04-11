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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        JobOfferDto GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        /// <exception cref="JobOffers.JobOfferNotDeletedException">Method throws JobOfferNotDeletedException when can't delete specified jobOffer</exception>
        void Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotCreatedException">Method throws JobOfferNotCreatedException when can't create specified jobOffer</exception>
        int Create(JobOfferDto jobOfferDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        /// <exception cref="JobOffers.JobOfferNotUpdatedException">Method throws JobOfferNotUpdatedException when can't update specified jobOffer</exception>
        JobOfferDto Update(JobOfferDto jobOfferDto);
    }
}
