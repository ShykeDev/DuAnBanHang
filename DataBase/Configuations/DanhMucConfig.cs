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
    public class DanhMucConfig : IEntityTypeConfiguration<DanhMuc>
    {
        public void Configure(EntityTypeBuilder<DanhMuc> builder)
        {
            builder.ToTable("DanhMucs");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(150)");
           
        }
    }
}
