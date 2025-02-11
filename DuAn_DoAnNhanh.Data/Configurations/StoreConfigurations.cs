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
    public class StoreConfigurations : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");
            builder.HasKey(x => x.StoreID);
            builder.Property(x => x.StoreID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Address).WithOne(x => x.Store);
            builder.HasOne(x => x.User).WithOne(x => x.Store);

            builder.HasMany(x => x.Bills).WithOne(x => x.Store).HasForeignKey(x=>x.StoreID);




        }

    }
}
