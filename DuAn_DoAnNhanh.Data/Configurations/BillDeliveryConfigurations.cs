using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Configurations
{
    public class BillDeliveryConfigurations : IEntityTypeConfiguration<BillDelivery>
    {
        public void Configure(EntityTypeBuilder<BillDelivery> builder)
        {
            builder.ToTable("BillDelivery");
            builder.HasKey(x => x.DeliveryID);
            builder.HasOne(x => x.Bill).WithOne(x => x.BillDelivery).HasForeignKey<BillDelivery>(x => x.BillID);

        }
    }
}
