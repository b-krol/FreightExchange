using Domain.CartageErrand;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class CartageErrandConfiguration : IEntityTypeConfiguration<CartageErrand>
    {
        public void Configure(EntityTypeBuilder<CartageErrand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.GoodsName)
                .IsRequired()
                .HasMaxLength(CartageErrand.GoodsNameMaxLength);
            builder.Property(x => x.StartingAdress)
                .IsRequired()
                .HasMaxLength(CartageErrand.StartingAdressMaxLength);
            builder.Property(x => x.DestinationAdress)
                .IsRequired()
                .HasMaxLength(CartageErrand.DestinationAdressMaxLength);
            builder.Property(x => x.Distance)
                .IsRequired();
            builder.Property(x => x.Weight)
                .IsRequired();
            builder.Property(x => x.MaximumPrice)
                .IsRequired();
            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasConversion(dt => dt, dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc));
            builder.Property(x => x.FounderId);
            builder.HasOne(x => x.Founder)
                .WithMany()
                .HasForeignKey(x => x.FounderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.SubmittedCartageOffers)
                .WithOne()
                .HasForeignKey(x => x.ErrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
