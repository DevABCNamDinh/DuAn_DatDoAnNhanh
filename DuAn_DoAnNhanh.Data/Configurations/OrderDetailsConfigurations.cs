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
            builder.ToTable("BillDetails");
            builder.HasKey(x => x.BillDetailsID);
            builder.Property(x => x.BillDetailsID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Order).WithMany(x => x.BillDetails).HasForeignKey(x => x.BillID).IsRequired();
            builder.Navigation(x => x.Order).AutoInclude();


        }
    }
}
