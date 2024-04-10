namespace WebApi.Controllers.Entities
{
    public class JobOffer
    {
        public int Id { get; set; }
        public int FounderId { get; set; }
        public string GoodsName { get; set; }
        public string StartingAdress { get; set; }
        public string DestinationAdress { get; set; }
        public int Distance { get; set; }
        public float Weight { get; set; }
        public int MaximumPrice { get; set; }
        public DateTime EndDate { get; set; }
        public JobOfferExeciutionStatus ExeciutionStatus { get; set; }
    }
}
