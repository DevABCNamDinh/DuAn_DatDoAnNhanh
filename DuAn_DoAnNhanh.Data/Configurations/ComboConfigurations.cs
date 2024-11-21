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
    public class ComboConfigurations : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.ToTable("Combos");
            builder.HasKey(x => x.ComboID);
            builder.Property(x => x.ComboID).ValueGeneratedOnAdd();
            builder.HasMany(x => x.BillDetails).WithOne(x => x.Combo).IsRequired();
            builder.HasMany(x => x.CartItemes).WithOne(x => x.Combo);

            builder.HasMany(x => x.ProductComboes).WithOne(x => x.Combo).IsRequired();





        }

    }
}
