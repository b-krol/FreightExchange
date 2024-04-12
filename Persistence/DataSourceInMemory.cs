using Application;
using Application.JobOffers;
using Application.Users;
using Domain.JobOffer;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal class DataSourceInMemory : IDataSource
    {

        private static Dictionary<int, User> Users = new Dictionary<int, User>()
        {
            { 1,
            new User()
            {
                Id = 1,
                Name = "Test",
                Email = "Test"
            }
            }
            ,
            { 2,
            new User()
            {
                Id = 2,
                Name = "Test2",
                Email = "Test2"
            }
            }
        };
        private static Dictionary<int, JobOffer> Jobs = new Dictionary<int, JobOffer>()
        {
            { 1,
            new JobOffer()
            {
                Id = 1,
                Founder = Users.First(x => x.Key == 1).Value,
                GoodsName = "Palety",
                StartingAdress = "Radom ul. Jana Pawła II 3",
                DestinationAdress = "Gdynia al. Niewiadoma",
                Distance = 524,
                Weight = 9.5f,
                MaximumPrice = 1000,
                EndDate = DateTime.Now - new TimeSpan(0, 0, 30),
                ExecutionStatus = JobOfferExecutionStatus.Success
            }
            }
            ,
            { 2,
            new JobOffer()
            {
                Id = 2,
                Founder = Users.First(x => x.Key == 2).Value,
                GoodsName = "Palety",
                StartingAdress = "Radom ul. Jana Pawła II 3",
                DestinationAdress = "Gdynia al. Niewiadoma",
                Distance = 524,
                Weight = 9.5f,
                MaximumPrice = 1000,
                EndDate = DateTime.Now + new TimeSpan(0, 0, 30),
                ExecutionStatus = JobOfferExecutionStatus.Active
            }
            }
        };
        private static int JobsNextId = Jobs.Count() + 1;
        private static int UsersNextId = Users.Count() + 1;

        public int CreateJobOffer(JobOffer jobOffer)
        {
            jobOffer.Id = JobsNextId++;
            Jobs.Add(jobOffer.Id, jobOffer);
            return jobOffer.Id;
        }

        public int CreateUser(User user)
        {
            user.Id = JobsNextId++;
            Users.Add(user.Id, user);
            return user.Id;
        }

        public JobOffer GetJobOfferById(int id)
        {
            var jobOffer = Jobs.GetValueOrDefault(id);
            if (jobOffer == null)
            {
                throw new JobOfferNotFoundException();
            }
            return jobOffer;
        }

        public IEnumerable<JobOffer> GetJobOffers()
        {
            return Jobs.Values;
        }

        public User GetUserById(int id)
        {
            var user = Users.GetValueOrDefault(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return Users.Values;
        }
        
        public int UpdateJobOffer(JobOffer jobOffer)
        {

            if (!Jobs.ContainsKey(jobOffer.Id))
            {
                throw new UserNotFoundException();
            }
            Jobs.Remove(jobOffer.Id);
            Jobs.Add(jobOffer.Id, jobOffer);
            return jobOffer.Id;
        }

        public int UpdateUser(User user)
        {
            if (!Users.ContainsKey(user.Id))
            {
                throw new UserNotFoundException();
            }
            Users.Remove(user.Id);
            Users.Add(user.Id, user);
            return user.Id;
        }

        void IDataSource.DeleteJobOffer(JobOffer jobOffer)
        {
            if(!Jobs.Remove(Jobs.SingleOrDefault(x => x.Value.Equals(jobOffer)).Key))//TODO rzeczywiste sprawdzanie czy się zgadza
                throw new JobOfferNotDeletedException();
        }

        void IDataSource.DeleteUser(User user)
        {
            if (!Users.Remove(Users.SingleOrDefault(x => x.Value.Equals(user)).Key))//TODO rzeczywiste sprawdzanie czy się zgadza
                throw new UserNotDeletedException();
        }
    }
}
