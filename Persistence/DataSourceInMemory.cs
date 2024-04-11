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
            user.Id = JobsNextId++;
            Users.Add(user);
            return user.Id;
        }

        public JobOffer GetJobOfferById(int id)
        {
            var jobOffer = Jobs.FirstOrDefault(jobOffer => jobOffer.Id == id);
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
            var user = Users.FirstOrDefault(user => user.Id == id);
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
        //TODO implement rest of DataSourceInMemory method
        public int UpdateJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        void IDataSource.DeleteJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
        }

        void IDataSource.DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
