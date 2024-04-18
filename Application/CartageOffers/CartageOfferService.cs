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
                BidderId = cartageOffer.Applicant.Id,
                Price = cartageOffer.Price,
                HasBeenConsidered = hasBeenConsidered,
                HasBeenAccepted = hasBeenAccepted
            };
        }

        public Task<int> Add(CartageOfferDto cartageoffer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartageOfferDto>> GetAllByCartageErrand(int id)
        {
            var cartageErrand = await Source.GetCartageErrandById(id);
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
