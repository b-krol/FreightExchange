using Domain.CartageOffer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.CartageErrand
{
    public class CartageErrand
    {
        public int Id { get; set; }
        public Domain.User.User Founder { get; private set; }
        public string GoodsName { get; private set; }
        public string StartingAdress { get; private set; }
        public string DestinationAdress { get; private set; }
        public int Distance { get; private set; }
        public float Weight { get; private set; }
        public int MaximumPrice { get; private set; }
        public DateTime EndDate { get; private set; }
        public CartageErrandExecutionStatus ExecutionStatus { get; private set; } = CartageErrandExecutionStatus.Active;
        private List<CartageOffer.CartageOffer> SubmittedCartageOffers = new List<CartageOffer.CartageOffer>();

        public CartageErrand(User.User founder, string goodsName, string startingAdress, string destinationAdress, int distance, float weight, int maximumPrice, DateTime endDate, CartageErrandExecutionStatus executionStatus)
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
            var timeLeft = dateTime.Subtract(DateTime.Now);
            if (timeLeft <= TimeSpan.FromMinutes(60)) throw new ArgumentOutOfRangeException($"{nameof(dateTime)} must refer at least 1 hour into future");
        }

        public ReadOnlyCollection<CartageOffer.CartageOffer> GetSubmittedCartageOffers()
        {
            return SubmittedCartageOffers.AsReadOnly();
        }

        public bool TryAddOffer(CartageOffer.CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public bool TryCancel()
        {
            throw new NotImplementedException();
        }

        public bool TryFinish()
        {
            throw new NotImplementedException();
        }
    }
}
