using Application;
using Application.CartageErrands;
using Application.Users;
using Domain.CartageErrand;
using Domain.CartageOffer;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal class DataSourceInMemory : IDataSource
    {//TODO implement CartageOffer related methods

        private static Dictionary<int, User> Users = new Dictionary<int, User>()
        {
            { 1,
            new User("Test1", "Test1@domain.com")
            }
            ,
            { 2,
            new User("Test2", "Test2@domain.com")
            }
        };
        private static Dictionary<int, CartageErrand> CartageErrands = new Dictionary<int, CartageErrand>()
        {
            { 1,
            new CartageErrand(
                    Users.First(x => x.Key == 1).Value,
                    "Palety",
                    "Radom ul. Jana Pawła II 3",
                    "Gdynia al. Niewiadoma",
                    524,
                    9.5f,
                    1000,
                    DateTime.Now + new TimeSpan(10, 0, 0),
                    CartageErrandExecutionStatus.Success
                        )
            }
            ,
            { 2,
                new CartageErrand(
                    Users.First(x => x.Key == 2).Value,
                    "Peryferia komputerowe",
                    "Radom ul. Jana Pawła II 3",
                    "Gdynia al. Niewiadoma",
                    600,
                    5.4f,
                    1100,
                    DateTime.Now + new TimeSpan(1, 0, 30),
                    CartageErrandExecutionStatus.Active
                        )
            }
        };
        private static int CartageErrandsNextId = CartageErrands.Count() + 1;
        private static int UsersNextId = Users.Count() + 1;

        public int CreateCartageErrand(CartageErrand cartageErrand)
        {
            cartageErrand.Id = CartageErrandsNextId++;
            CartageErrands.Add(cartageErrand.Id, cartageErrand);
            return cartageErrand.Id;
        }

        public int CreateCartageOffer(CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public int CreateUser(User user)
        {
            user.Id = UsersNextId++;
            Users.Add(user.Id, user);
            return user.Id;
        }

        public void DeleteCartageOffer(CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public CartageErrand GetCartageErrandById(int id)
        {
            var cartageErrand = CartageErrands.GetValueOrDefault(id);
            if (cartageErrand == null)
            {
                throw new CartageErrandNotFoundException();
            }
            return cartageErrand;
        }

        public IEnumerable<CartageErrand> GetCartageErrands()
        {
            return CartageErrands.Values;
        }

        public CartageErrand GetCartageOfferById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartageOffer> GetCartageOffers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartageOffer> GetCartageOffersByCartageErrand(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            var user = Users.GetValueOrDefault(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return Users.Values;
        }
        
        public int UpdateCartageErrand(CartageErrand cartageErrand)
        {

            if (!CartageErrands.ContainsKey(cartageErrand.Id))
            {
                throw new UserNotFoundException();
            }
            CartageErrands.Remove(cartageErrand.Id);
            CartageErrands.Add(cartageErrand.Id, cartageErrand);
            return cartageErrand.Id;
        }

        public int UpdateCartageOffer(CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(User user)
        {
            if (!Users.ContainsKey(user.Id))
            {
                throw new UserNotFoundException();
            }
            Users.Remove(user.Id);
            Users.Add(user.Id, user);
            return user.Id;
        }

        void IDataSource.DeleteCartageErrand(CartageErrand cartageErrand)
        {
            if(!CartageErrands.Remove(CartageErrands.SingleOrDefault(x => x.Value.Equals(cartageErrand)).Key))//TODO rzeczywiste sprawdzanie czy się zgadza
                throw new CartageErrandNotDeletedException();
        }

        void IDataSource.DeleteUser(User user)
        {
            if (!Users.Remove(Users.SingleOrDefault(x => x.Value.Equals(user)).Key))//TODO rzeczywiste sprawdzanie czy się zgadza
                throw new UserNotDeletedException();
        }
    }
}
