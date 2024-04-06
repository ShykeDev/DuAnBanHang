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
            builder.HasOne<HoaDon>().WithMany(p => p.HoaDonChiTiets).HasForeignKey(p => p.ID)
                .HasConstraintName("FK_HDCT");
        }
    }
}
