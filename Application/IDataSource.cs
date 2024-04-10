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
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find user</exception>
        void DeleteUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find user</exception>
        /// <exception cref="Users.UserNotDeletedException">Method throws UserNotDeletedException when can't delete user</exception>
        int CreateUser(User user);
        User UpdateUser(User user);


        IEnumerable<JobOffer> GetJobOffers();
        JobOffer GetJobOfferById(int id);
        void DeleteJobOffer(JobOffer jobOffer);
        int CreateJobOffer(JobOffer jobOffer);
        JobOffer UpdateJobOffer(JobOffer jobOffer);
    }
}
