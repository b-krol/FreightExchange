using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private static JobOfferDto CreateJobOfferDto(JobOffer jobOffer)
        {
            if (jobOffer.ExeciutionStatus == JobOfferExeciutionStatus.Active)
                return new JobOfferDto()
                {
                    Id = jobOffer.Id,
                    FounderId = jobOffer.FounderId,
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
                FounderId = jobOffer.FounderId,
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

        private static List<JobOffer> _jobs = new List<JobOffer>()
        {
            new JobOffer()
            {
                Id = 1,
                FounderId = 1,
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
                FounderId = 1,
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

        private static int _idCount = _jobs.Count + 1;

        [HttpGet]
        public IEnumerable<JobOfferDto> GetJobOffers()
        {
            var jobOfferDtos = new List<JobOfferDto>();
            foreach (var user in _jobs)
            {
                jobOfferDtos.Add(CreateJobOfferDto(user));
            }
            return jobOfferDtos;
        }

        [HttpGet("Active")]
        public IEnumerable<JobOfferDto> GetActiveJobOffers()
        {
            var jobOfferDtos = new List<JobOfferDto>();
            foreach (var user in _jobs)
            {
                if(user.ExeciutionStatus == JobOfferExeciutionStatus.Active)
                    jobOfferDtos.Add(CreateJobOfferDto(user));
            }
            return jobOfferDtos;
        }

        [HttpGet("Finished")]
        public IEnumerable<JobOfferDto> GetFinishedJobOffers()
        {
            var jobOfferDtos = new List<JobOfferDto>();
            foreach (var user in _jobs)
            {
                if (user.ExeciutionStatus != JobOfferExeciutionStatus.Active)
                    jobOfferDtos.Add(CreateJobOfferDto(user));
            }
            return jobOfferDtos;
        }

        [HttpPost]
        public IActionResult CreateJobOffer(JobOfferDto jobOfferDto)
        {
            JobOffer newJobOffer = new JobOffer()
            { 
                Id = _idCount++,
                FounderId = jobOfferDto.FounderId,
                GoodsName = jobOfferDto.GoodsName,
                StartingAdress = jobOfferDto.StartingAdress,
                DestinationAdress = jobOfferDto.DestinationAdress,
                Distance = jobOfferDto.Distance,
                Weight = jobOfferDto.Weight,
                MaximumPrice = jobOfferDto.MaximumPrice,
                EndDate = jobOfferDto.EndDate,
                ExeciutionStatus = JobOfferExeciutionStatus.Active
            };
            _jobs.Add(newJobOffer);
            return Created($"{Request.GetEncodedUrl()}/{newJobOffer.Id}", CreateJobOfferDto(newJobOffer));
        }

    }
}
