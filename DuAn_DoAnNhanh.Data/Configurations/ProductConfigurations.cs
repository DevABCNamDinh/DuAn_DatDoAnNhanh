using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductID);
            builder.Property(x => x.ProductID).ValueGeneratedOnAdd();

            builder.HasMany(x => x.CartItemes).WithOne(x => x.Product);
            builder.HasMany(x => x.ProductComboes).WithOne(x => x.Product).IsRequired();
            

        }

    }
}
