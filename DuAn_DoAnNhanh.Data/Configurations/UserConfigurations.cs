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
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserID);
            builder.Property(x=> x.UserID).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Addresses).WithOne(x => x.User).HasForeignKey(x => x.UserID).IsRequired();
            builder.HasMany(x=>x.Orderes).WithOne(x=>x.User).HasForeignKey(x=>x.UserID).IsRequired();
           
        }
    }
}
