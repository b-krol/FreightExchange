using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(User.NameMaxLength);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(User.EmailMaxLength);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(User.PasswordMaxLength);
        }
    }
}
