using Application.Users;
using Domain.JobOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JobOffers
{
    internal class JobOfferService : IJobOfferService
    {
        private readonly IDataSource Source;
        public JobOfferService(IDataSource source)
        {
            Source = source;
        }
        private static JobOfferDto CreateJobOfferDto(JobOffer jobOffer)
        {
            if (jobOffer.ExeciutionStatus == JobOfferExeciutionStatus.Active)
                return new JobOfferDto()
                {
                    Id = jobOffer.Id,
                    FounderId = jobOffer.Founder.Id,
                    GoodsName = jobOffer.GoodsName,
                    StartingAdress = jobOffer.StartingAdress,
                    DestinationAdress = jobOffer.DestinationAdress,
                    Distance = jobOffer.Distance,
                    Weight = jobOffer.Weight,
                    MaximumPrice = jobOffer.MaximumPrice,
                    EndDate = jobOffer.EndDate,
                    IsActive = true
                };
            return new JobOfferDto()
            {
                Id = jobOffer.Id,
                FounderId = jobOffer.Founder.Id,
                GoodsName = jobOffer.GoodsName,
                StartingAdress = jobOffer.StartingAdress,
                DestinationAdress = jobOffer.DestinationAdress,
                Distance = jobOffer.Distance,
                Weight = jobOffer.Weight,
                MaximumPrice = jobOffer.MaximumPrice,
                EndDate = jobOffer.EndDate,
                IsActive = false
            };
        }


        public int Create(JobOfferDto jobOfferDto)
        {
            JobOffer newJobOffer = new JobOffer();
            newJobOffer.Id = 0;
            try
            {
                newJobOffer.Founder = Source.GetUserById(jobOfferDto.FounderId);
            }
            catch (UserNotFoundException)
            {
                //TODO nie można utworzyć obiektu JobOffer, bo nie ma użytkownika o id podanym w DTO
            }
            newJobOffer.GoodsName = jobOfferDto.GoodsName;
            newJobOffer.StartingAdress = jobOfferDto.StartingAdress;
            newJobOffer.DestinationAdress = jobOfferDto.DestinationAdress;
            newJobOffer.Distance = jobOfferDto.Distance;
            newJobOffer.Weight = jobOfferDto.Weight;
            newJobOffer.MaximumPrice = jobOfferDto.MaximumPrice;
            newJobOffer.EndDate = jobOfferDto.EndDate; //TODO co kiedy data zakończenia ma miejsce w przeszłości
            newJobOffer.ExeciutionStatus = JobOfferExeciutionStatus.Active;
            Source.CreateJobOffer(newJobOffer);
            return newJobOffer.Id;
        }

        public void Delete(int id)
        {
            try
            {
                Source.DeleteJobOffer(Source.GetJobOfferById(id));
            }
            catch (JobOfferNotFoundException)
            {
                throw;
            }
            catch (JobOfferNotDeletedException)
            {
                throw;
            }
        }

        public IEnumerable<JobOfferDto> GetAll()
        {
            var jobOfferDtos = new List<JobOfferDto>();
            foreach(var jobOffer in Source.GetJobOffers())
            {
                jobOfferDtos.Add(CreateJobOfferDto(jobOffer));
            }
            return jobOfferDtos;
        }

        public JobOfferDto GetById(int id)
        {
            JobOffer? jobOffer =  Source.GetJobOfferById(id);
            if (jobOffer == null)
            {
                throw new JobOfferNotFoundException();
            }
            return CreateJobOfferDto(jobOffer);
        }

        public JobOfferDto Update(JobOfferDto jobOfferDto)
        {
            JobOffer newJobOffer = new JobOffer();
            newJobOffer.Id = 0;
            try
            {
                newJobOffer.Founder = Source.GetUserById(jobOfferDto.FounderId);
            }
            catch (UserNotFoundException)
            {
                //TODO nie można utworzyć obiektu JobOffer, bo nie ma użytkownika o id podanym w DTO
            }
            newJobOffer.GoodsName = jobOfferDto.GoodsName;
            newJobOffer.StartingAdress = jobOfferDto.StartingAdress;
            newJobOffer.DestinationAdress = jobOfferDto.DestinationAdress;
            newJobOffer.Distance = jobOfferDto.Distance;
            newJobOffer.Weight = jobOfferDto.Weight;
            newJobOffer.MaximumPrice = jobOfferDto.MaximumPrice;
            newJobOffer.EndDate = jobOfferDto.EndDate; //TODO co kiedy data zakończenia ma miejsce w przeszłości
            newJobOffer.ExeciutionStatus = JobOfferExeciutionStatus.Active;
            try
            {
                int newJobOfferId = Source.UpdateJobOffer(newJobOffer);
                return GetById(newJobOfferId);
            }
            catch (JobOfferNotFoundException)
            {
                throw;
            }
        }

    }
}
