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
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.AddressID);
            builder.Property(x => x.AddressID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.User).WithMany(x => x.Addresses).HasForeignKey(x=>x.UserID).IsRequired();
            builder.Navigation(x => x.User).AutoInclude();



        }

    }
}
