﻿using Application;
using Application.CartageErrands;
using Application.CartageOffers;
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
    {

        private static Dictionary<int, User> Users = new Dictionary<int, User>()
        {
            { 1,
            new User("Test1", "P@ssw0rd", email: "Test1@domain.com"){ Id = 1}
            }
            ,
            { 2,
            new User("Test2", "P@ssw0rd", email: "Test2@domain.com"){ Id = 2 }
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
                    DateTime.UtcNow + new TimeSpan(10, 0, 0)
                        ){ Id = 1}
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
                    DateTime.UtcNow + new TimeSpan(1, 0, 30)
                        ){ Id = 2 }
            }
        };
        private static Dictionary<int, CartageOffer> CartageOffers = new Dictionary<int, CartageOffer>() { };
        private static int CartageErrandsNextId = CartageErrands.Count() + 1;
        private static int CartageOffersNextId = CartageOffers.Count() + 1;
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
        public Task AddUser(User user)
        {
            user.Id = UsersNextId++;
            Users.Add(user.Id, user);
            return Task.CompletedTask;
        }
        public Task DeleteUser(User user)
        {
            if (Users.Remove(user.Id))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new UserNotDeletedException();
            }
            
        }
        #endregion

        #region CartageErrands
        public Task<IEnumerable<CartageErrand>> GetCartageErrands()
        {
            return Task.FromResult((IEnumerable<CartageErrand>)CartageErrands.Values);
        }
        public Task<CartageErrand> GetCartageErrandById(int id)
        {
            var cartageErrand = CartageErrands.GetValueOrDefault(id);
            if (cartageErrand == null)
            {
                throw new CartageErrandNotFoundException();
            }
            return Task.FromResult(cartageErrand);
        }
        public Task AddCartageErrand(CartageErrand cartageErrand)
        {
            cartageErrand.Id = CartageErrandsNextId++;
            CartageErrands.Add(cartageErrand.Id, cartageErrand);
            return Task.CompletedTask;
        }
        public Task DeleteCartageErrand(CartageErrand cartageErrand)
        {
            if (CartageErrands.Remove(cartageErrand.Id))
            {
                return Task.CompletedTask;
            }
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

        #region CartageOffers
        public Task<IEnumerable<CartageOffer>> GetCartageOffers()
        {
            return Task.FromResult((IEnumerable<CartageOffer>)CartageOffers);
        }
        public Task<CartageOffer> GetCartageOfferById(int id)
        {
            var x = CartageOffers.GetValueOrDefault(id);
            if (x == null)
            {
                throw new CartageOfferNotFoundException();
            }
            return Task.FromResult(x);
        }
        public Task AddCartageOffer(CartageOffer cartageOffer)
        {
            cartageOffer.Id = CartageOffersNextId++;
            CartageOffers.Add(cartageOffer.Id, cartageOffer);
            return Task.CompletedTask;
        }
        #endregion

        public Task<IEnumerable<CartageOffer>> GetCartageOffersForUser(int id)
        {
            return Task.FromResult((IEnumerable<CartageOffer>)CartageErrands.Values.Where(x => x.GetSubmittedCartageOffers().Where(x => x.Bidder.Id == id) != null));
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CartageErrand>> GetCartageErrandsExceedingEndTime()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindUser(string email, string password)//TODO implement FindUser method in DataSourceInMemory
        {
            throw new NotImplementedException();
        }
    }
}
