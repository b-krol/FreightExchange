using Domain.CartageOffer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            if (value <= 0) throw new ArgumentException();
        }

        private void ThrowIfEqualOrLessThanZero(float value)
        {
            if (value <= 0) throw new ArgumentException();
        }

        private void ThrowIfDateTimeRefersToPastOrNotEnoughIntoFuture(DateTime dateTime)
        {
            var timeLeft = dateTime.Subtract(DateTime.Now);
            if (timeLeft <= TimeSpan.FromMinutes(60)) throw new ArgumentException();
        }

        public ReadOnlyCollection<CartageOffer.CartageOffer> GetSubmittedCartageOffers()
        {
            return SubmittedCartageOffers.AsReadOnly();
        }

        public void AddOffer(CartageOffer.CartageOffer cartageOffer)
        {
            if(ExecutionStatus != CartageErrandExecutionStatus.Active)
                throw new CartageErrandAddingNewCartageOfferNotAcceptedException($"CartageErrand {nameof(ExecutionStatus)} is not active");
            if (cartageOffer.Price > MaximumPrice)
                throw new CartageErrandAddingNewCartageOfferNotAcceptedException($"Price surpassed {nameof(MaximumPrice)}");
            SubmittedCartageOffers.Add(cartageOffer);
        }

        public void Cancel()
        {
            if (ExecutionStatus != CartageErrandExecutionStatus.Active)
                throw new CartageErrandExecutionStatusChangeNotAllowedException($"CartageErrand {nameof(ExecutionStatus)} was already finished or cancelled");
            ExecutionStatus = CartageErrandExecutionStatus.Cancelled;
        }

        public CartageOffer.CartageOffer? Finish()
        {
            if(ExecutionStatus != CartageErrandExecutionStatus.Active)
                throw new CartageErrandExecutionStatusChangeNotAllowedException($"CartageErrand {nameof(ExecutionStatus)} was already finished or cancelled");
            var winningOffer = GetWinningOfferOrDefault();
            if(winningOffer == null)
            {
                ExecutionStatus = CartageErrandExecutionStatus.Failure;
            }
            else
            {
                ExecutionStatus = CartageErrandExecutionStatus.Success;
            }
            return winningOffer;
        }

        public CartageOffer.CartageOffer? GetWinningOfferOrDefault(CartageOffer.CartageOffer? defaultOffer = null)
        {
            CartageOffer.CartageOffer? winningOffer = null;
            foreach(CartageOffer.CartageOffer cartageOffer in SubmittedCartageOffers)
            {
                if(winningOffer == null)
                {
                    winningOffer = cartageOffer;
                }
                else if(cartageOffer.Price < winningOffer.Price)
                {
                    winningOffer.Price = cartageOffer.Price;
                }
            }
            if(winningOffer != null)
                return winningOffer;
            return defaultOffer;
        }
    }
}
