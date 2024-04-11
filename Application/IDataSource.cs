using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.User;
using Domain.JobOffer;

namespace Application
{
    public interface IDataSource
    {
        IEnumerable<User> GetUsers();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        User GetUserById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotDeletedException">Method throws UserNotDeletedException when can't delete specified user</exception>
        void DeleteUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotCreatedException">Method throws UserNotCreatedException when can't create specified user</exception>
        int CreateUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotUpdatedException">Method throws UserNotUpdatedException when can't update specified user</exception>
        int UpdateUser(User user);
        


        IEnumerable<JobOffer> GetJobOffers();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        JobOffer GetJobOfferById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        /// <exception cref="JobOffers.JobOfferNotDeletedException">Method throws JobOfferNotDeletedException when can't delete specified jobOffer</exception>
        void DeleteJobOffer(JobOffer jobOffer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotCreatedException">Method throws JobOfferNotCreatedException when can't create specified jobOffer</exception>
        int CreateJobOffer(JobOffer jobOffer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified jobOffer</exception>
        /// <exception cref="JobOffers.JobOfferNotUpdatedException">Method throws JobOfferNotUpdatedException when can't update specified jobOffer</exception>
        int UpdateJobOffer(JobOffer jobOffer);
    }
}
