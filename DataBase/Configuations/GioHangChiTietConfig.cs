using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class GioHangChiTietConfig : IEntityTypeConfiguration<GioHangChiTiet>
    {
        public void Configure(EntityTypeBuilder<GioHangChiTiet> builder)
        {
            builder.ToTable("GioHangChiTiets");
            builder.HasKey(x => new { x.ID, x.IDSanPham });
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.HasOne(p => p.gioHang).WithMany(p => p.GioHangChiTiets).HasForeignKey(p => p.ID)
                .HasConstraintName("FK_GHCT_GH");
            builder.HasOne(p => p.sanPham).WithOne(p => p.GioHangChiTiets).HasForeignKey<GioHangChiTiet>(p => p.IDSanPham)
                .HasConstraintName("FK_GHCT_SP");
        }
    }
}
