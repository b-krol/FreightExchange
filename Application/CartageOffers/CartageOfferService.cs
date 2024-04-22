using AutoMapper;
using Domain.CartageErrand;
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
        private readonly IMapper Mapper;

        public CartageOfferService(IDataSource source, IMapper mapper)
        {
            Source = source;
            Mapper = mapper;
        }

        public async Task<int> Add(int cartageErrandId, CartageOfferDto cartageOfferDto)
        {
            CartageErrand cartageErrand = await Source.GetCartageErrandById(cartageErrandId);
            var bidder = await Source.GetUserById(cartageOfferDto.BidderId);

            var newCartageOffer = new CartageOffer(
                bidder,
                cartageOfferDto.Price
                );
            await Source.AddCartageOffer(newCartageOffer);

            cartageErrand.AddOffer(newCartageOffer);

            await Source.SaveChangesAsync();
            return newCartageOffer.Id;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();//TODO decide if there should or not be allowed to delete already created offer (first write tests)
        }

        public async Task<IEnumerable<CartageOfferDto>> GetAllByCartageErrand(int cartageErrandId)
        {
            var cartageErrand = await Source.GetCartageErrandById(cartageErrandId);
            return Mapper.Map<IEnumerable<CartageOfferDto>>(cartageErrand.GetSubmittedCartageOffers());
        }

        public async Task<CartageOfferDto> GetById(int id)
        {
            var cartageOffer = await Source.GetCartageOfferById(id);
            return Mapper.Map<CartageOfferDto>(cartageOffer);
        }

    }
}
