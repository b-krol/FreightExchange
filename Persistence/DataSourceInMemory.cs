﻿using Application;
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

        #region users
        public Task<IEnumerable<User>> GetUsers()
        {
            return Task.FromResult((IEnumerable<User>)Users.Values);
        }
        public Task<User> GetUserById(int id)
        {
            var user = Users.GetValueOrDefault(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return Task.FromResult(user);
        }
        public int AddUser(User user)
        {
            user.Id = UsersNextId++;
            Users.Add(user.Id, user);
            return user.Id;
        }
        void IDataSource.DeleteUser(User user)
        {
            if (!Users.Remove(user.Id))
                throw new UserNotDeletedException();
        }
        //public int UpdateUser(User user)
        //{
        //    if (!Users.ContainsKey(user.Id))
        //    {
        //        throw new UserNotFoundException();
        //    }
        //    Users.Remove(user.Id);
        //    Users.Add(user.Id, user);
        //    return user.Id;
        //}
        #endregion

        #region CartageErrands
        public IEnumerable<CartageErrand> GetCartageErrands()
        {
            return CartageErrands.Values;
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
        public int AddCartageErrand(CartageErrand cartageErrand)
        {
            cartageErrand.Id = CartageErrandsNextId++;
            CartageErrands.Add(cartageErrand.Id, cartageErrand);
            return cartageErrand.Id;
        }
        void IDataSource.DeleteCartageErrand(CartageErrand cartageErrand)
        {
            if (!CartageErrands.Remove(cartageErrand.Id))
                throw new CartageErrandNotDeletedException();
        }
        //public int UpdateCartageErrand(CartageErrand cartageErrand)
        //{

        //    if (!CartageErrands.ContainsKey(cartageErrand.Id))
        //    {
        //        throw new UserNotFoundException();
        //    }
        //    CartageErrands.Remove(cartageErrand.Id);
        //    CartageErrands.Add(cartageErrand.Id, cartageErrand);
        //    return cartageErrand.Id;
        //}
        #endregion

        public IEnumerable<CartageOffer> GetCartageOffersForUser(int id)//TODO implement GetCartageOffersForUser in DataSourceInMemory
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()//TODO implement SaveChangesAsync in DataSourceInMemory
        {
            throw new NotImplementedException();
        }
    }
}
