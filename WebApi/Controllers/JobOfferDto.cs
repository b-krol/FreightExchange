namespace WebApi.Controllers
{
    public class JobOfferDto
    {
        public int? Id { get; init; }
        public int FounderId { get; init; }
        public string GoodsName { get; init; }
        public string StartingAdress { get; init; }
        public string DestinationAdress { get; init; }
        public int Distance { get; init; }
        public float Weight { get; init; }
        public int MaximumPrice { get; init; }
        public DateTime EndDate { get; init; }
        public bool? IsActive { get; init; }
    }
}
