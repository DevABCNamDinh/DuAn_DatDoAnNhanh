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
    public class BillPaymentConfigurations : IEntityTypeConfiguration<BillPayment>
    {
        public void Configure(EntityTypeBuilder<BillPayment> builder)
        {
            builder.ToTable("BillPayments");
            builder.HasKey(x => x.BillPaymentID);
            builder.HasOne(x => x.Bill).WithMany(x => x.BillPayments).HasForeignKey(x => x.BillID).IsRequired();
        }
    }
}
