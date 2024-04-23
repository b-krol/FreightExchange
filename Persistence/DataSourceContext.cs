using Application;
using Application.Users;
using Domain.CartageErrand;
using Domain.CartageOffer;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSourceContext : DbContext, IDataSource
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CartageErrand> CartageErrands { get; set; }
        public DbSet<CartageOffer> CartageOffers { get; set; }

        public DataSourceContext(DbContextOptions<DataSourceContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #region User
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public Task DeleteUser(User user)
        {
            Users.Remove(user);
            return Task.CompletedTask;
        }

        public Task AddUser(User user)
        {
            Users.AddAsync(user);
            return Task.CompletedTask;
        }
        #endregion

        #region CartageErrand
        public Task<IEnumerable<CartageErrand>> GetCartageErrands()
        {
            throw new NotImplementedException();
        }

        public Task<CartageErrand> GetCartageErrandById(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartageErrand(CartageErrand cartageErrand)
        {
            throw new NotImplementedException();
        }

        public Task AddCartageErrand(CartageErrand cartageErrand)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CartageOffer
        public Task<IEnumerable<CartageOffer>> GetCartageOffers()
        {
            throw new NotImplementedException();
        }

        public Task<CartageOffer> GetCartageOfferById(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartageOffer(CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public Task AddCartageOffer(CartageOffer cartageOffer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartageOffer>> GetCartageOffersForUser(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
