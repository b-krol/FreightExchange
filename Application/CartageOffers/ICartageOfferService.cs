using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public interface ICartageOfferService
    {
        Task<IEnumerable<CartageOfferDto>> GetAllByCartageErrand(int cartageErrandId);
        Task<CartageOfferDto> GetById(int id);
        public Task<IEnumerable<CartageOfferDto>> GetAllByUser(int userId);
        Task<int> Add(int cartageErrandId, CartageOfferDto cartageOfferDto);
    }
}
