using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class GioHangConfig : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.ToTable("GioHangs");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.HasOne(p => p.user).WithOne(p => p.GioHangs).HasForeignKey<GioHang>(p => p.ID)
                .HasConstraintName("FK_GH_KH");
        }
    }
}
