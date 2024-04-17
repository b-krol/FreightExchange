using Application.Users;
using Domain.CartageErrand;
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
        private static CartageErrandDto CreateCartageErrandDto(Domain.CartageErrand.CartageErrand cartageErrand)
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
                IsActive = isActive
            };
        }


        public int Add(CartageErrandDto cartageErrandDto)
        {
            Domain.CartageErrand.CartageErrand newCartageErrand = new Domain.CartageErrand.CartageErrand(
                    Source.GetUserById(cartageErrandDto.FounderId),
                    cartageErrandDto.GoodsName,
                    cartageErrandDto.StartingAdress,
                    cartageErrandDto.DestinationAdress,
                    cartageErrandDto.Distance,
                    cartageErrandDto.Weight,
                    cartageErrandDto.MaximumPrice,
                    cartageErrandDto.EndDate,
                    CartageErrandExecutionStatus.Active
                );
            return Source.AddCartageErrand(newCartageErrand);
        }

        public void Delete(int id)
        {
            Source.DeleteCartageErrand(Source.GetCartageErrandById(id));
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
