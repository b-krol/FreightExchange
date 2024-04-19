using Domain.CartageOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    internal class CartageOfferService : ICartageOfferService
    {
        private readonly IDataSource Source;
        public CartageOfferService(IDataSource source)
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

        public async Task<int> Add(int cartageErrandId, CartageOfferDto cartageOfferDto)
        {
            //var cartageErrand = await Source.GetCartageErrandById(cartageErrandId);
            //var bidder = await Source.GetUserById(cartageOfferDto.BidderId);
            //return cartageErrand.AddOffer(
            //        new CartageOffer(
            //                bidder,
            //                cartageOfferDto.Price,
            //                CartageOfferConsiderationStatus.Waiting
            //            )
            //    );
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartageOfferDto>> GetAllByCartageErrand(int cartageErrandId)
        {
            var cartageErrand = await Source.GetCartageErrandById(cartageErrandId);
            return cartageErrand.GetSubmittedCartageOffers().Select(CreateCartageOfferDto);
        }

        public Task<CartageOfferDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(CartageOfferDto cartageoffer)
        {
            throw new NotImplementedException();
        }
    }
}
