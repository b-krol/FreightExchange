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
        public int Create(CartageOfferDto cartageoffer)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartageOfferDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartageOfferDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CartageOfferDto Update(CartageOfferDto cartageoffer)
        {
            throw new NotImplementedException();
        }
    }
}
