using Application.CartageOffers;
using Application.Users;
using Domain.CartageErrand;
using Domain.CartageOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    internal class CartageErrandService : ICartageErrandService
    {
        private readonly IDataSource Source;
        public CartageErrandService(IDataSource source)
        {
            Source = source;
        }
        private static CartageOfferDto CreateCartageOfferDto(CartageOffer cartageOffer)
        {
            bool hasBeenConsidered;
            bool hasBeenAccepted = false;
            if (cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Waiting)
            {
                hasBeenConsidered = false;
            }
            else
            {
                hasBeenConsidered = true;
                if (cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Accepted)
                {
                    hasBeenAccepted = true;
                }
            }
            return new CartageOfferDto()
            {
                Id = cartageOffer.Id,
                BidderId = cartageOffer.Bidder.Id,
                Price = cartageOffer.Price,
                HasBeenConsidered = hasBeenConsidered,
                HasBeenAccepted = hasBeenAccepted
            };
        }
        private static CartageErrandDto CreateCartageErrandDto(CartageErrand cartageErrand)
        {
            bool isActive;
            if (cartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Active)
                isActive = true;
            else
                isActive = false;

            return new CartageErrandDto()
            {
                Id = cartageErrand.Id,
                FounderId = cartageErrand.Founder.Id,
                GoodsName = cartageErrand.GoodsName,
                StartingAdress = cartageErrand.StartingAdress,
                DestinationAdress = cartageErrand.DestinationAdress,
                Distance = cartageErrand.Distance,
                Weight = cartageErrand.Weight,
                MaximumPrice = cartageErrand.MaximumPrice,
                EndDate = cartageErrand.EndDate,
                IsActive = isActive,
                Offers = cartageErrand.GetSubmittedCartageOffers().Select(CreateCartageOfferDto).ToList()
            };
        }
        public async Task<int> Add(CartageErrandDto cartageErrandDto)
        {
            var founder = await Source.GetUserById(cartageErrandDto.FounderId);
            CartageErrand newCartageErrand = new CartageErrand(
                    founder,
                    cartageErrandDto.GoodsName,
                    cartageErrandDto.StartingAdress,
                    cartageErrandDto.DestinationAdress,
                    cartageErrandDto.Distance,
                    cartageErrandDto.Weight,
                    cartageErrandDto.MaximumPrice,
                    cartageErrandDto.EndDate
                );
            await Source.AddCartageErrand(newCartageErrand);
            await Source.SaveChangesAsync();
            return newCartageErrand.Id;
        }

        public async Task<IEnumerable<CartageErrandDto>> GetAll()
        {
            var cartageErrandDtos = new List<CartageErrandDto>();
            var cartageErrands = await Source.GetCartageErrands();
            foreach (var cartageErrand in cartageErrands)
            {
                cartageErrandDtos.Add(CreateCartageErrandDto(cartageErrand));
            }
            return cartageErrandDtos;
        }

        public async Task<CartageErrandDto> GetById(int id)
        {
            CartageErrand cartageErrand = await Source.GetCartageErrandById(id);
            return CreateCartageErrandDto(cartageErrand);
        }

    }
}
