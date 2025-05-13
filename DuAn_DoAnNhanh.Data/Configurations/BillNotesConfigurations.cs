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
    public class BillNotesConfigurations : IEntityTypeConfiguration<BillNotes>
    {
        public void Configure(EntityTypeBuilder<BillNotes> builder)
        {
            builder.ToTable("BillNotes");
            builder.HasKey(x => x.BillNotesID);
            builder.HasOne(x => x.Bill).WithMany(x => x.BillNotes).HasForeignKey(x => x.BillID).IsRequired();
        }
    }
}
