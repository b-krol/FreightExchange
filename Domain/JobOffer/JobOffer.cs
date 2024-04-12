using System;

namespace Domain.JobOffer
{
    public class JobOffer
    {
        public int Id { get; set; }
        public Domain.User.User Founder { get; set; }
        public string GoodsName { get; set; }
        public string StartingAdress { get; set; }
        public string DestinationAdress { get; set; }
        public int Distance { get; set; }
        public float Weight { get; set; }
        public int MaximumPrice { get; set; }
        public DateTime EndDate { get; set; }
        public JobOfferExecutionStatus ExecutionStatus { get; set; }
    }
}
