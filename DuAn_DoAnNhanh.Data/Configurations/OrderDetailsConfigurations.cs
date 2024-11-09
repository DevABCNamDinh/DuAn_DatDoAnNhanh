using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Configurations
{
    public class OrderDetailsConfigurations : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.OrderDetailsID);
            builder.Property(x => x.OrderDetailsID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Order).WithMany(x => x.BillDetails).HasForeignKey(x => x.OrderID).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.BillDetails).HasForeignKey(x => x.ProductID).IsRequired();
        }
    }
}
