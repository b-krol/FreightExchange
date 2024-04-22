using Application.CartageOffers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.CartageErrands
{
    public class CartageErrandDto
    {
        public int? Id { get; init; }
        public int FounderId { get; init; }
        public required string GoodsName { get; init; }
        public required string StartingAdress { get; init; }
        public required string DestinationAdress { get; init; }
        public int Distance { get; init; }
        public float Weight { get; init; }
        public int MaximumPrice { get; init; }
        public DateTime EndDate { get; init; }
        public bool IsActive { get; init; } = false;
    }

    public class CartageErrandWithOffersDto : CartageErrandDto
    {
        public required ICollection<CartageOfferDto> Offers { get; init; }
    }
}
