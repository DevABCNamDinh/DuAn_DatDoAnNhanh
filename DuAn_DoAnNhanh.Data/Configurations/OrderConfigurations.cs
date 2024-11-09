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
    public class OrderConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.OrderID);
            builder.Property(x => x.OrderID).ValueGeneratedOnAdd();
            builder.HasMany(x => x.BillDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderID).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Orderes).HasForeignKey(x => x.UserID).IsRequired();
        }
    
    }
}
