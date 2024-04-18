using Application.Users;
using Domain.CartageErrand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    public interface ICartageErrandService
    {
        Task<IEnumerable<CartageErrandDto>> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        Task<CartageErrandDto> GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        /// <exception cref="CartageErrand.CartageErrandNotDeletedException">Method throws CartageErrandNotDeletedException when can't delete specified CartageErrand</exception>
        Task<int> Add(CartageErrandDto cartageErrandDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        /// <exception cref="CartageErrand.CartageErrandNotUpdatedException">Method throws CartageErrandNotUpdatedException when can't update specified CartageErrand</exception>

        //TODO in future mayby allow to update CartageErrand but in scenarios like - no one have assigned offer to the cartage or no one has asked for this cartage
        //CartageErrandDto Update(CartageErrandDto cartageErrandDto);
    }
}
