﻿using DataBase.Entities;
using DataBase.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configuations
{
    public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPhams");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(x => x.GiaGoc).HasDefaultValue(0);
            builder.Property(x => x.GiaGiamGia).HasDefaultValue(0);
            builder.Property(x => x.SoLuong).HasDefaultValue(0);
            builder.Property(x => x.TrangThai).HasDefaultValue(eTrangThaiSanPham.show);
        }
    }
}
