using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.User;
using Domain.CartageErrand;
using Domain.CartageOffer;

namespace Application
{
    public interface IDataSource
    {
        Task SaveChangesAsync();
        #region Users
        Task<IEnumerable<User>> GetUsers();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        Task<User> GetUserById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotDeletedException">Method throws UserNotDeletedException when can't delete specified user</exception>
        void DeleteUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotCreatedException">Method throws UserNotCreatedException when can't create specified user</exception>
        int AddUser(User user);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="user"></param>
        ///// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        ///// <exception cref="Users.UserNotUpdatedException">Method throws UserNotUpdatedException when can't update specified user</exception>
        //int UpdateUser(User user);
        #endregion

        #region CartageErrands
        IEnumerable<CartageErrand> GetCartageErrands();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        CartageErrand GetCartageErrandById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        /// <exception cref="CartageErrand.CartageErrandNotDeletedException">Method throws CartageErrandNotDeletedException when can't delete specified CartageErrand</exception>
        void DeleteCartageErrand(CartageErrand cartageErrand);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartageErrand"></param>
        /// <exception cref="CartageErrand.CartageErrandNotCreatedException">Method throws CartageErrandNotCreatedException when can't create specified CartageErrand</exception>
        int AddCartageErrand(CartageErrand cartageErrand);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="CartageErrand"></param>
        ///// <exception cref="CartageErrand.CartageErrandNotFoundException">Method throws CartageErrandNotFoundException when can't find specified CartageErrand</exception>
        ///// <exception cref="CartageErrand.CartageErrandNotUpdatedException">Method throws CartageErrandNotUpdatedException when can't update specified CartageErrand</exception>
        //int UpdateCartageErrand(CartageErrand cartageErrand);
        #endregion

        #region CartageOffers
        IEnumerable<CartageOffer> GetCartageOffersForUser(int userId);
        //CartageErrand GetCartageOfferById(int id);
        //void DeleteCartageOffer(CartageOffer cartageOffer);
        //int AddCartageOffer(CartageOffer cartageOffer);
        //int UpdateCartageOffer(CartageOffer cartageOffer);
        #endregion
    }
}
