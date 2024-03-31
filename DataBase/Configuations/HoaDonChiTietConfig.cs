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
            builder.HasKey(x => new { x.ID, x.IDSanPham });
            builder.HasOne(p => p.hoaDon).WithMany(p => p.HoaDonChiTiets).HasForeignKey(p => p.ID)
                .HasConstraintName("FK_HDCT");
            builder.HasOne(p => p.sanPham).WithOne(p => p.HoaDonChiTiets).HasForeignKey<HoaDonChiTiet>(p => p.IDSanPham)
               .HasConstraintName("FK_HDCT_SP");
        }
    }
}
