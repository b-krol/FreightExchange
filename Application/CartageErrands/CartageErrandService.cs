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
        private static CartageErrandDto CreateCartageErrandDto(CartageErrand cartageErrand)
        {
            bool isActive;
            if (cartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Active)
                isActive = true;
            else
                isActive = false;

            List<int> cartageOffersIds = new List<int>();
            foreach(var cartageOffer in cartageErrand.GetSubmittedCartageOffers())
            {
                cartageOffersIds.Add(cartageOffer.Id);
            }

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
                IsActive = isActive
            };
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
                if(cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Accepted)
                {
                    hasBeenAccepted = true;
                }
            }
            return new CartageOfferDto()
            {
                Id = cartageOffer.Id,
                BidderId = cartageOffer.Applicant.Id,
                Price = cartageOffer.Price,
                HasBeenConsidered = hasBeenConsidered,
                HasBeenAccepted = hasBeenAccepted
            };
        }

        public async Task<int> Add(CartageErrandDto cartageErrandDto)
        {
            CartageErrand newCartageErrand = new CartageErrand(
                    await Source.GetUserById(cartageErrandDto.FounderId),
                    cartageErrandDto.GoodsName,
                    cartageErrandDto.StartingAdress,
                    cartageErrandDto.DestinationAdress,
                    cartageErrandDto.Distance,
                    cartageErrandDto.Weight,
                    cartageErrandDto.MaximumPrice,
                    cartageErrandDto.EndDate,
                    CartageErrandExecutionStatus.Active
                );
            return await Source.AddCartageErrand(newCartageErrand);
        }

        public async Task Delete(int id)
        {
            await Source.DeleteCartageErrand(
                    await Source.GetCartageErrandById(id)
                );
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
            var cartageErrandDto = CreateCartageErrandDto(cartageErrand);
            cartageErrandDto.Offers = cartageErrand.GetSubmittedCartageOffers().Select(x => CreateCartageOfferDto(x)).ToList();
            return cartageErrandDto;
        }

        //public CartageErrandDto Update(CartageErrandDto cartageErrandDto)
        //{
        //    CartageErrand newCartageErrand = new Domain.CartageErrand.CartageErrand(
        //            Source.GetUserById(cartageErrandDto.FounderId),
        //            cartageErrandDto.GoodsName,
        //            cartageErrandDto.StartingAdress,
        //            cartageErrandDto.DestinationAdress,
        //            cartageErrandDto.Distance,
        //            cartageErrandDto.Weight,
        //            cartageErrandDto.MaximumPrice,
        //            cartageErrandDto.EndDate,
        //            CartageErrandExecutionStatus.Active
        //        );
        //    int newCartageErrandId = Source.UpdateCartageErrand(newCartageErrand);
        //    return GetById(newCartageErrandId);
        //}

    }
}
