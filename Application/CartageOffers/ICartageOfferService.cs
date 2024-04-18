using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public interface ICartageOfferService
    {
        Task<IEnumerable<CartageOfferDto>> GetAllByCartageErrand(int id);
        Task<CartageOfferDto> GetById(int id);
        Task Delete(int id);
        Task<int> Add(CartageOfferDto cartageoffer);
        Task<int> UpdateAsync(CartageOfferDto cartageoffer);
    }
}
