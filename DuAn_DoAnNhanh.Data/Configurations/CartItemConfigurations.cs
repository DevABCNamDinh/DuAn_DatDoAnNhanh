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
    public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(x => x.CartItemID);
            builder.Property(x => x.CartItemID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Cart).WithMany(x => x.CartItems).HasForeignKey(x => x.CartID).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.CartItemes).HasForeignKey(x => x.ProductID).IsRequired();
            builder.HasOne(x => x.Combo).WithMany(x => x.CartItemes).HasForeignKey(x => x.ComboID).IsRequired();
            
        }

    }
}
