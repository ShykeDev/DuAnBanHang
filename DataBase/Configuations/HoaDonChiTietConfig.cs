using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class HoaDonChiTietConfig : IEntityTypeConfiguration<HoaDonChiTiet>
    {
        public void Configure(EntityTypeBuilder<HoaDonChiTiet> builder)
        {
            builder.ToTable("HoaDonChiTiets");
            builder.HasKey(x => x.ID);
            builder.HasOne<HoaDon>().WithMany(p => p.HoaDonChiTiets).HasForeignKey(p => p.IDSHoaDon)
                .HasConstraintName("FK_HDCT");
        }
    }
}
