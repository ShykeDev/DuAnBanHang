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
    public class DanhMucChiTietConfig : IEntityTypeConfiguration<DanhMucChiTiet>
    {
        public void Configure(EntityTypeBuilder<DanhMucChiTiet> builder)
        {
            builder.ToTable("DanhMucChiTiets");
            builder.HasKey(x => new { x.idDanhMuc, x.idSanPham });
            builder.HasOne<DanhMuc>().WithMany(x => x.DanhMucChiTiets)
                   .HasForeignKey(p => p.idDanhMuc)
                   .HasPrincipalKey(p => p.ID)
                   .HasConstraintName("FK_DMCT_DM");
        }
    }
}
