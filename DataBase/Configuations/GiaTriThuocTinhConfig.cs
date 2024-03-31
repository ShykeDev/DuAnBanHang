using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class GiaTriThuocTinhConfig : IEntityTypeConfiguration<GiaTriThuocTinh>
    {
        public void Configure(EntityTypeBuilder<GiaTriThuocTinh> builder)
        {
            builder.ToTable("GiaTriThuocTinhs");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.GiaTri).IsRequired().HasMaxLength(50);
            builder.HasOne<ThuocTinhChung>().WithMany(p => p.GiaTriThuocTinhs)
                                            .HasForeignKey(p => p.IDThuocTinh)
                                            .HasPrincipalKey(p => p.ID)
                                            .HasConstraintName("FK_TT_GTTT");

        }
    }
}
