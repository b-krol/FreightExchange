using Application.JobOffers;
using Application.Users;
using Domain.JobOffer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOffersController : ControllerBase
    {

        private IJobOfferService JobOfferService { get; }

        public JobOffersController(IJobOfferService jobOfferService)
        {
            JobOfferService = jobOfferService;
        }
        [HttpGet]
        public IEnumerable<JobOfferDto> GetJobOffers()
        {
            return JobOfferService.GetAll();
        }

        [HttpGet("Active")]
        public IEnumerable<JobOfferDto> GetActiveJobOffers()
        {
            var jobOffers = new List<JobOfferDto>();
            foreach(var jobOffer in GetJobOffers())
            {
                if((bool)jobOffer.IsActive)
                    jobOffers.Add(jobOffer);
            }
            return jobOffers;
        }

        [HttpGet("Finished")]
        public IEnumerable<JobOfferDto> GetFinishedJobOffers()
        {
            var jobOffers = new List<JobOfferDto>();
            foreach (var jobOffer in GetJobOffers())
            {
                if (!(bool)jobOffer.IsActive)
                    jobOffers.Add(jobOffer);
            }
            return jobOffers;
        }

        [HttpPost]
        public IActionResult CreateJobOffer(JobOfferDto jobOfferDto)
        {
            var newJobOfferId = JobOfferService.Create(jobOfferDto);
            return Created($"{Request.GetEncodedUrl()}/{newJobOfferId}", JobOfferService.GetById(newJobOfferId));
        }

        [HttpPut]
        public IActionResult UpdateJobOffer(JobOfferDto jobOfferDto)
        {
            return Ok(JobOfferService.Update(jobOfferDto));
        }

    }
}
