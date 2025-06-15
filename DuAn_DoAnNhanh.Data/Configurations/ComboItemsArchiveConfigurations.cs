using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DuAn_DoAnNhanh.Data.Configurations
{
    public class ComboItemsArchiveConfigurations : IEntityTypeConfiguration<ComboItemsArchive>
    {
        public void Configure(EntityTypeBuilder<ComboItemsArchive> builder)
        {
            builder.ToTable("ComboItemsArchives");
            builder.HasKey(x => x.ArchiveID);
            builder.HasOne(x => x.BillDetails).WithMany(x => x.ComboItemsArchives).HasForeignKey(x => x.BillDetailsID); 
        }
    }
}
