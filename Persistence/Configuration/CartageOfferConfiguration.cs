using Domain.CartageOffer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class CartageOfferConfiguration : IEntityTypeConfiguration<CartageOffer>
    {
        public void Configure(EntityTypeBuilder<CartageOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.ConsiderationStatus)
                .IsRequired();
            //builder.Property(x => x.ErrandId);
            builder.Property(x => x.BidderId);
            builder.HasOne(x => x.Bidder)
                .WithMany()
                .HasForeignKey(x => x.BidderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
