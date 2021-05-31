using Microsoft.EntityFrameworkCore;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Configuration
{
    public class PurchaseCompositionEntityConfiguration : IEntityTypeConfiguration<CompositionPurchase>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompositionPurchase> builder)
        {
            builder
                .HasOne(i => i.Purchase)
                .WithMany(i => i.Products)
                .HasForeignKey(i => i.PurchaseId);
            builder
                 .HasOne(i => i.Product)
                 .WithMany(i => i.Purchases)
                 .HasForeignKey(i => i.ProductId);
            builder
                .HasKey(sc => new { sc.ProductId, sc.PurchaseId});

        }
    }
}
