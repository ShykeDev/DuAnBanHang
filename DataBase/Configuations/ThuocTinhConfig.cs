using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class ThuocTinhConfig : IEntityTypeConfiguration<ThuocTinh>
    {
        public void Configure(EntityTypeBuilder<ThuocTinh> builder)
        {
            builder.ToTable("ThuocTinhs");
            builder.HasKey(x => new { x.ID, x.IDSanPham });
            builder.HasOne<SanPham>().WithMany(p => p.thuocTinhs).HasForeignKey(p => p.IDSanPham)
                .HasConstraintName("FK_TT_SP");
            
        }
    }
}
