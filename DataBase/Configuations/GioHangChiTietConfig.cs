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
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.HasOne<User>().WithMany(p => p.GioHangChiTiets).HasForeignKey(p => p.UserID)
                .HasConstraintName("FK_GHCT_GH");
        }
    }
}
