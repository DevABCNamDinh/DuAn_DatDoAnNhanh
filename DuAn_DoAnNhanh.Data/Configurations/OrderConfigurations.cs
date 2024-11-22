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
            builder.ToTable("Bill");
            builder.HasKey(x => x.BillID);
            builder.Property(x => x.BillID).ValueGeneratedOnAdd();
            builder.HasMany(x => x.BillDetails).WithOne(x => x.Order).HasForeignKey(x => x.BillID);
            builder.HasOne(x => x.User).WithMany(x => x.Orderes).HasForeignKey(x => x.UserID).IsRequired();
            builder.Navigation(x => x.User).AutoInclude();
            builder.Navigation(x => x.BillDetails).AutoInclude();


        }

    }
}
