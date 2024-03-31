using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuations
{
    public class ThuocTinhChungConfig : IEntityTypeConfiguration<ThuocTinhChung>
    {
        public void Configure(EntityTypeBuilder<ThuocTinhChung> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.TenThuocTinh).HasColumnType("nvarchar(50)");
            builder.HasOne<ThuocTinh>().WithOne(p => p.thuocTinhChung).HasForeignKey<ThuocTinh>(p => p.ID)
                .HasConstraintName("FK_TTC_TT");
        }
    }
}