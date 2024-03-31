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
    public class ItemImageConfig : IEntityTypeConfiguration<ItemImage>
    {
        public void Configure(EntityTypeBuilder<ItemImage> builder)
        {
            builder.HasKey(x => new { x.ID, x.Path });
            builder.HasOne(p => p.sanPhams).WithMany(p => p.anhs).HasForeignKey(p => p.ID)
                .HasConstraintName("FK_IMG_SP");
        }
    }
}
