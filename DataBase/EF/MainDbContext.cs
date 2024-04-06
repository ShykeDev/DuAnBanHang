
using DataBase.Configuations;
using Microsoft.EntityFrameworkCore;
using DataBase.Entities;
using Microsoft.EntityFrameworkCore.Design;
using DataBase.Extensions;

namespace DataBase.EF
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=ADMIN-PC\\SHYKE;Initial Catalog=QuanLyShop;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.UseSqlServer("Data Source=SHYKE\\SQLEXPRESS;Initial Catalog=QuanLyShop;Integrated Security=True;Trust Server Certificate=True");

            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new GioHangChiTietConfig());
            modelBuilder.ApplyConfiguration(new HoaDonConfig());
            modelBuilder.ApplyConfiguration(new HoaDonChiTietConfig());
            modelBuilder.ApplyConfiguration(new SanPhamConfig());
            modelBuilder.ApplyConfiguration(new DanhMucConfig());
            modelBuilder.ApplyConfiguration(new DanhMucChiTietConfig());
            modelBuilder.ApplyConfiguration(new ThuocTinhChungConfig());
            modelBuilder.ApplyConfiguration(new ThuocTinhConfig());
            modelBuilder.ApplyConfiguration(new GiaTriThuocTinhConfig());
            modelBuilder.ApplyConfiguration(new ItemImageConfig());
            MainExtensions.SeedData(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DanhMucChiTiet> DanhMucChiTiets { get; set; }
        public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
        public DbSet<ThuocTinh> ThuocTinhs { get; set; }
        public DbSet<ThuocTinhChung> ThuocTinhChungs { get; set; }
        public DbSet<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    }
}
