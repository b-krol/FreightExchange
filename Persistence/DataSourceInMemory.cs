using Application;
using Application.JobOffers;
using Application.Users;
using Domain.JobOffer;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
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
            new User("Test1", "Test1@domain.com")
            }
            ,
            { 2,
            new User("Test2", "Test2@domain.com")
            }
        };
        private static Dictionary<int, JobOffer> Jobs = new Dictionary<int, JobOffer>()
        {
            { 1,
            new JobOffer(
                    Users.First(x => x.Key == 1).Value,
                    "Palety",
                    "Radom ul. Jana Pawła II 3",
                    "Gdynia al. Niewiadoma",
                    524,
                    9.5f,
                    1000,
                    DateTime.Now - new TimeSpan(0, 0, 30),
                    JobOfferExecutionStatus.Success
                        )
            }
            ,
            { 2,
                new JobOffer(
                    Users.First(x => x.Key == 2).Value,
                    "Peryferia komputerowe",
                    "Radom ul. Jana Pawła II 3",
                    "Gdynia al. Niewiadoma",
                    600,
                    5.4f,
                    1100,
                    DateTime.Now + new TimeSpan(0, 0, 30),
                    JobOfferExecutionStatus.Active
                        )
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
            user.Id = UsersNextId++;
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
