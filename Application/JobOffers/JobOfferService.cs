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
            bool isActive;
            if (jobOffer.ExecutionStatus == JobOfferExecutionStatus.Active)
                isActive = true;
            else
                isActive = false;
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
                IsActive = isActive
            };
        }


        public int Create(JobOfferDto jobOfferDto)
        {
            JobOffer newJobOffer = new JobOffer(
                    Source.GetUserById(jobOfferDto.FounderId),
                    jobOfferDto.GoodsName,
                    jobOfferDto.StartingAdress,
                    jobOfferDto.DestinationAdress,
                    jobOfferDto.Distance,
                    jobOfferDto.Weight,
                    jobOfferDto.MaximumPrice,
                    jobOfferDto.EndDate,
                    JobOfferExecutionStatus.Active
                );
            return Source.CreateJobOffer(newJobOffer);
        }

        public void Delete(int id)
        {
            Source.DeleteJobOffer(Source.GetJobOfferById(id));
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
            JobOffer jobOffer =  Source.GetJobOfferById(id);
            return CreateJobOfferDto(jobOffer);
        }

        public JobOfferDto Update(JobOfferDto jobOfferDto)
        {
            JobOffer newJobOffer = new JobOffer(
                    Source.GetUserById(jobOfferDto.FounderId),
                    jobOfferDto.GoodsName,
                    jobOfferDto.StartingAdress,
                    jobOfferDto.DestinationAdress,
                    jobOfferDto.Distance,
                    jobOfferDto.Weight,
                    jobOfferDto.MaximumPrice,
                    jobOfferDto.EndDate,
                    JobOfferExecutionStatus.Active//TODO can be updated only active JobOffers?
                );
            int newJobOfferId = Source.UpdateJobOffer(newJobOffer);
            return GetById(newJobOfferId);
        }

    }
}
