﻿using Domain.CartageOffer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domain.CartageErrand
{
    public class CartageErrand
    {
        public const int GoodsNameMaxLength = 100;
        public const int StartingAdressMaxLength = 200;
        public const int DestinationAdressMaxLength = 200;
        public int Id { get; set; }
        public int FounderId { get; init; }
        public Domain.User.User Founder { get; private set; }
        public string GoodsName { get; private set; }
        public string StartingAdress { get; private set; }
        public string DestinationAdress { get; private set; }
        public int Distance { get; private set; }
        public float Weight { get; private set; }
        public int MaximumPrice { get; private set; }
        public DateTime EndDate { get; private set; }
        public CartageErrandExecutionStatus ExecutionStatus { get; private set; } = CartageErrandExecutionStatus.Active;
        internal List<CartageOffer.CartageOffer> SubmittedCartageOffers = new List<CartageOffer.CartageOffer>();

        private CartageErrand()
        {
            //required by ORM
        }
        public CartageErrand(User.User founder, string goodsName, string startingAdress, string destinationAdress, int distance, float weight, int maximumPrice, DateTime endDate)
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
        }

        private void ThrowIfNullOrEmpty(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException();
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
            if (dateTime <= DateTime.UtcNow) throw new ArgumentException();
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
            foreach (var offer in SubmittedCartageOffers)
            {
                offer.ConsiderationStatus = CartageOfferConsiderationStatus.Rejected;
            }
        }

        public CartageOffer.CartageOffer? Finish()
        {
            if(ExecutionStatus != CartageErrandExecutionStatus.Active)
                throw new CartageErrandExecutionStatusChangeNotAllowedException($"CartageErrand {nameof(ExecutionStatus)} was already finished or cancelled");
            var cheapestOffer = GetCheapestOfferOrDefault();
            if(cheapestOffer == null)
            {
                ExecutionStatus = CartageErrandExecutionStatus.Failure;
            }
            else
            {
                ExecutionStatus = CartageErrandExecutionStatus.Success;
                SubmittedCartageOffers[SubmittedCartageOffers.IndexOf(cheapestOffer)].ConsiderationStatus = CartageOfferConsiderationStatus.Accepted;
                foreach (var offer in SubmittedCartageOffers.Where(x => x.ConsiderationStatus != CartageOfferConsiderationStatus.Accepted))
                {
                    offer.ConsiderationStatus = CartageOfferConsiderationStatus.Rejected;
                }
                
            }
            return cheapestOffer;
        }

        public CartageOffer.CartageOffer? GetCheapestOfferOrDefault(CartageOffer.CartageOffer? defaultOffer = null)
        {
            CartageOffer.CartageOffer? cheapestOffer = null;
            foreach(CartageOffer.CartageOffer cartageOffer in SubmittedCartageOffers)
            {
                if(cheapestOffer == null)
                {
                    cheapestOffer = cartageOffer;
                }
                else if(cartageOffer.Price < cheapestOffer.Price)
                {
                    cheapestOffer.Price = cartageOffer.Price;
                }
            }
            if(cheapestOffer != null)
                return cheapestOffer;
            return defaultOffer;
        }
    }
}
