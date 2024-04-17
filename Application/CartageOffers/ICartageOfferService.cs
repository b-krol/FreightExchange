using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public interface ICartageOfferService
    {
        IEnumerable<CartageOfferDto> GetAll();
        CartageOfferDto GetById(int id);
        void Delete(int id);
        int Create(CartageOfferDto cartageoffer);
        CartageOfferDto Update(CartageOfferDto cartageoffer);
    }
}
