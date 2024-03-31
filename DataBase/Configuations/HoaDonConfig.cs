using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class HoaDonConfig : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDons");
            builder.HasKey(x => x.ID);
            builder.HasOne<User>().WithMany(p => p.HoaDons).HasForeignKey(p => p.UserID)
                .HasConstraintName("FK_HD_KH");
        }
    }
}
