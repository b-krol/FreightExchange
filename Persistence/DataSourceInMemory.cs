using Application;
using Application.JobOffers;
using Application.Users;
using Domain.JobOffer;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal class DataSourceInMemory : IDataSource
    {

        private static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name = "Test",
                Email = "Test"
            },
            new User()
            {
                Id = 2,
                Name = "Test2",
                Email = "Test2"
            }
        };
        private static List<JobOffer> Jobs = new List<JobOffer>()
        {
            new JobOffer()
            {
                Id = 1,
                Founder = Users.First(),
                GoodsName = "Palety",
                StartingAdress = "Radom ul. Jana Pawła II 3",
                DestinationAdress = "Gdynia al. Niewiadoma",
                Distance = 524,
                Weight = 9.5f,
                MaximumPrice = 1000,
                EndDate = DateTime.Now - new TimeSpan(0, 0, 30),
                ExeciutionStatus = JobOfferExeciutionStatus.Success
            }
            ,
            new JobOffer()
            {
                Id = 2,
                Founder = Users.Last(),
                GoodsName = "Palety",
                StartingAdress = "Radom ul. Jana Pawła II 3",
                DestinationAdress = "Gdynia al. Niewiadoma",
                Distance = 524,
                Weight = 9.5f,
                MaximumPrice = 1000,
                EndDate = DateTime.Now + new TimeSpan(0, 0, 30),
                ExeciutionStatus = JobOfferExeciutionStatus.Active
            }
        };
        private static int JobsNextId = Jobs.Count() + 1;
        private static int UsersNextId = Users.Count() + 1;

        public int CreateJobOffer(JobOffer jobOffer)
        {
            jobOffer.Id = JobsNextId++;
            Jobs.Add(jobOffer);
            return jobOffer.Id;
        }

        public int CreateUser(User user)
        {
            user.Id = UsersNextId++;
            Users.Add(user);
            return user.Id;
        }

        public JobOffer GetJobOfferById(int id)
        {
            var jobOffer = Jobs.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null)
            {
                throw new JobOfferNotFoundException();
            }
            return jobOffer;
        }

        public IEnumerable<JobOffer> GetJobOffers()
        {
            return Jobs;
        }

        public User GetUserById(int id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return Users;
        }
        
        public int UpdateJobOffer(JobOffer jobOffer)
        {

            var updatedJobOffer = GetJobOfferById(jobOffer.Id);
            if (updatedJobOffer == null)
            {
                throw new UserNotFoundException();
            }
            var index = Jobs.IndexOf(updatedJobOffer);
            Jobs.RemoveAt(index);
            Jobs.Insert(index, jobOffer);
            return updatedJobOffer.Id;
        }

        public int UpdateUser(User user)
        {
            var updatedUser = GetUserById(user.Id);
            if (updatedUser == null)
            {
                throw new UserNotFoundException();
            }
            var index = Users.IndexOf(updatedUser);
            Users.RemoveAt(index);
            Users.Insert(index, user);
            return updatedUser.Id;
        }

        void IDataSource.DeleteJobOffer(JobOffer jobOffer)
        {
            if(!Jobs.Remove(jobOffer))
                throw new JobOfferNotDeletedException();
        }

        void IDataSource.DeleteUser(User user)
        {
            if (!Users.Remove(user))
                throw new UserNotDeletedException();
        }
    }
}
