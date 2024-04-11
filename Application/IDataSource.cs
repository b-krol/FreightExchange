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
        User GetUserById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        void DeleteUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotDeletedException">Method throws UserNotDeletedException when can't delete specified user</exception>
        int CreateUser(User user);
        int UpdateUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>


        IEnumerable<JobOffer> GetJobOffers();
        JobOffer GetJobOfferById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified JobOffer</exception>
        void DeleteJobOffer(JobOffer jobOffer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified JobOffer</exception>
        /// <exception cref="JobOffers.JobOfferNotDeletedException">Method throws JobOfferNotDeletedException when can't delete specified JobOffer</exception>
        int CreateJobOffer(JobOffer jobOffer);
        int UpdateJobOffer(JobOffer jobOffer);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <exception cref="JobOffers.JobOfferNotFoundException">Method throws JobOfferNotFoundException when can't find specified JobOffer</exception>
    }
}
