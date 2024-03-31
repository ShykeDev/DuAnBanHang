using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Extensions
{
    public class MainExtensions
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(Guid.NewGuid(), "Nguyễn Lê Nhất Vũ", "shyke", "19112004", "0865805582", "nhatvu@gmail.com", "Phúc Diễn, Bắc Từ Liêm, Hà Nội", "2004-01-01", 0)
            );
            /*
                modelBuilder.Entity<SanPham>().HasData(
                    new SanPham(Guid.NewGuid(), "Sản phẩm 1", 121233, 111111, 100, Entities.Enum.eTrangThaiSanPham.show, ""),
                    new SanPham(Guid.NewGuid(), "Sản phẩm 2", 123312, 23123, 100, Entities.Enum.eTrangThaiSanPham.show, ""),
                    new SanPham(Guid.NewGuid(), "Sản phẩm 3", 123123, 123123, 100, Entities.Enum.eTrangThaiSanPham.show, ""),
                    new SanPham(Guid.NewGuid(), "Sản phẩm 4", 123321, 112312, 100, Entities.Enum.eTrangThaiSanPham.show, "")
                );
            */
        }
    }
}
