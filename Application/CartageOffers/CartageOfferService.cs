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

        public Task<int> Add(CartageOfferDto cartageoffer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartageOfferDto>> GetAll()
        {
            throw new NotImplementedException();
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
