using Application;
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

        public int CreateJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
        }

        public int CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public JobOffer GetJobOfferById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobOffer> GetJobOffers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public JobOffer UpdateJobOffer(JobOffer jobOffer)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
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
