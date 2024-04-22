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
    public class DataSourceContext : DbContext//TODO has to implement IDataSource interface
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

    }
}
