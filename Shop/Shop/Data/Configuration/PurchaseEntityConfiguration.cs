using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Configuration
{
    public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(i => i.Id);

            builder
                .HasOne(i => i.User)
                .WithMany(i => i.Purchases)
                .HasForeignKey(i => i.UserId);

            builder.Property(i => i.Date).HasColumnType("datetime");
            
        }
    }
}
