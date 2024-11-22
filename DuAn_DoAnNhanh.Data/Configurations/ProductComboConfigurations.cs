using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Configurations
{
    public class ProductComboConfigurations : IEntityTypeConfiguration<ProductCombo>
    {
        public void Configure(EntityTypeBuilder<ProductCombo> builder)
        {
            builder.ToTable("ProductCombos");
            builder.HasKey(x => x.ProductComboID);
            builder.Property(x => x.ProductComboID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductComboes).IsRequired();
            builder.HasOne(x => x.Combo).WithMany(x => x.ProductComboes).IsRequired();




        }

    }
}
