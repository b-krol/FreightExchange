using Application;
using Application.CartageErrands;
using Application.CartageOffers;
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
        public async Task<IEnumerable<CartageErrand>> GetCartageErrands()
        {
            return await CartageErrands.ToListAsync();
        }

        public async Task<IEnumerable<CartageErrand>> GetCartageErrandsExceedingEndTime()
        {
            var cartageErrands = await CartageErrands
                .Where(x => x.EndDate < DateTime.UtcNow)
                .Where(x => x.ExecutionStatus == CartageErrandExecutionStatus.Active)
                .ToListAsync();
            cartageErrands.Sort((x, y) => x.EndDate.CompareTo(y.EndDate));
            return cartageErrands;
        }

        public async Task<CartageErrand> GetCartageErrandById(int id)
        {
            var cartageErrand = await CartageErrands
                .Where(x => x.Id == id)
                .Include(x => x.Founder)
                .Include(x => x.SubmittedCartageOffers)
                .FirstOrDefaultAsync();
            if(cartageErrand == null)
            {
                throw new CartageErrandNotFoundException();
            }
            return cartageErrand;//TODO now returns unspecified DateTime but should return Utc DateTime. But property is unaccessible from there
        }

        public Task DeleteCartageErrand(CartageErrand cartageErrand)
        {
            CartageErrands.Remove(cartageErrand);
            return Task.CompletedTask;
        }

        public Task AddCartageErrand(CartageErrand cartageErrand)
        {
            CartageErrands.AddAsync(cartageErrand);
            return Task.CompletedTask;
        }
        #endregion

        #region CartageOffer
        public async Task<IEnumerable<CartageOffer>> GetCartageOffers()
        {
            return await CartageOffers.ToListAsync();
        }

        public async Task<CartageOffer> GetCartageOfferById(int id)
        {
            var cartageOffer = await CartageOffers.Where(x => x.Id == id)
                .Include(x => x.Bidder)
                .FirstOrDefaultAsync();
            if(cartageOffer == null)
            {
                throw new CartageOfferNotFoundException();
            }
            return cartageOffer;
        }

        public Task<IEnumerable<CartageOffer>> GetCartageOffersForUser(int userId)//TODO implement GetCartageOffersForUser method
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
