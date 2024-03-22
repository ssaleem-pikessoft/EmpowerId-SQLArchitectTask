using EmpowerId.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.ProductId);
            builder.Property(c => c.ProductName).HasMaxLength(500);

            builder.HasOne(p => p.Category)
           .WithOne(c => c.Product)
           .HasForeignKey<Product>(p => p.CategoryId)
           .IsRequired(false);

        }
    }
}
