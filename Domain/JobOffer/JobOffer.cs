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

        public JobOffer(User.User founder, string goodsName, string startingAdress, string destinationAdress, int distance, float weight, int maximumPrice, DateTime endDate, JobOfferExecutionStatus executionStatus)
        {
            ThrowIfNullOrEmpty(goodsName);
            ThrowIfNullOrEmpty(startingAdress);
            ThrowIfNullOrEmpty(destinationAdress);

            ThrowIfEqualOrLessThanZero(distance);
            ThrowIfEqualOrLessThanZero(weight);
            ThrowIfEqualOrLessThanZero(maximumPrice);

            ThrowIfDateTimeRefersToPastOrNotEnoughIntoFuture(endDate);

            Founder = founder;
            GoodsName = goodsName.Trim();
            StartingAdress = startingAdress.Trim();
            DestinationAdress = destinationAdress.Trim();
            Distance = distance;
            Weight = weight;
            MaximumPrice = maximumPrice;
            EndDate = endDate;
            ExecutionStatus = executionStatus;
        }

        private void ThrowIfNullOrEmpty(string value)
        {
            if (value == null) throw new ArgumentException();
            if (value == string.Empty) throw new ArgumentException();
            if (value.Trim() == string.Empty) throw new ArgumentException();
        }

        private void ThrowIfEqualOrLessThanZero(int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException();
        }

        private void ThrowIfEqualOrLessThanZero(float value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException();
        }

        private void ThrowIfDateTimeRefersToPastOrNotEnoughIntoFuture(DateTime dateTime)
        {
            if(dateTime - DateTime.Now <= TimeSpan.FromMinutes(60)) throw new ArgumentOutOfRangeException();
        }
    }
}
