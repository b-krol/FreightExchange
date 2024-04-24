using Application.CartageOffers;
using Application.Users;
using AutoMapper;
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
        private readonly IMapper Mapper;

        public CartageErrandService(IDataSource source, IMapper mapper)
        {
            Source = source;
            Mapper = mapper;
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
                    cartageErrandDto.EndDate.ToUniversalTime()
                );
            await Source.AddCartageErrand(newCartageErrand);
            await Source.SaveChangesAsync();
            return newCartageErrand.Id;
        }

        public async Task<IEnumerable<CartageErrandDto>> GetAll()
        {
            var cartageErrands = await Source.GetCartageErrands();
            return Mapper.Map<IEnumerable<CartageErrandDto>>(cartageErrands);
        }

        public async Task<CartageErrandWithOffersDto> GetById(int id)
        {
            CartageErrand cartageErrand = await Source.GetCartageErrandById(id);
            return Mapper.Map<CartageErrandWithOffersDto>(cartageErrand);
        }

    }
}
