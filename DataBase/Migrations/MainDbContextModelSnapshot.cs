﻿// <auto-generated />
using System;
using DataBase.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBase.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataBase.Entities.DanhMuc", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("ID");

                    b.ToTable("DanhMucs", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.DanhMucChiTiet", b =>
                {
                    b.Property<Guid>("idDanhMuc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("idSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("idDanhMuc", "idSanPham");

                    b.HasIndex("idSanPham");

                    b.ToTable("DanhMucChiTiets", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.GiaTriThuocTinh", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GiaTri")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("IDThuocTinh")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("IDThuocTinh");

                    b.ToTable("GiaTriThuocTinhs", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.GioHangChiTiet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("ThuocTinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("IDSanPham");

                    b.HasIndex("UserID");

                    b.ToTable("GioHangChiTiets", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.HoaDon", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChuThich")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayMua")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThaiDonHang")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("HoaDons", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.HoaDonChiTiet", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiaSanPham")
                        .HasColumnType("int");

                    b.Property<Guid>("IDSHoaDon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("ThuocTinh")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("IDSHoaDon");

                    b.HasIndex("IDSanPham");

                    b.ToTable("HoaDonChiTiets", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.ItemImage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("DataBase.Entities.SanPham", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiaGiamGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("GiaGoc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("SoLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.HasKey("ID");

                    b.ToTable("SanPhams", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.ThuocTinh", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID", "IDSanPham");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("IDSanPham");

                    b.ToTable("ThuocTinhs", (string)null);
                });

            modelBuilder.Entity("DataBase.Entities.ThuocTinhChung", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TenThuocTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("ThuocTinhChungs");
                });

            modelBuilder.Entity("DataBase.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NgaySinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            ID = new Guid("43f19c8c-e49c-4fe4-adf6-83e04d53802b"),
                            DiaChi = "Phúc Diễn, Bắc Từ Liêm, Hà Nội",
                            Email = "nhatvu@gmail.com",
                            Name = "Nguyễn Lê Nhất Vũ",
                            NgaySinh = "2004-01-01",
                            Password = "Admin19112004",
                            Role = 0,
                            SDT = "0865805582",
                            State = 0,
                            UserName = "shyke"
                        },
                        new
                        {
                            ID = new Guid("0fc8e4f7-93fb-44be-8a39-a561d47ac6c9"),
                            DiaChi = "Phúc Diễn, Bắc Từ Liêm, Hà Nội",
                            Email = "nhatvu@gmail.com",
                            Name = "Nguyễn Lê Nhất Vũ",
                            NgaySinh = "2004-01-01",
                            Password = "User19112004",
                            Role = 1,
                            SDT = "0865805582",
                            State = 0,
                            UserName = "shykeuser"
                        });
                });

            modelBuilder.Entity("DataBase.Entities.DanhMucChiTiet", b =>
                {
                    b.HasOne("DataBase.Entities.DanhMuc", null)
                        .WithMany("DanhMucChiTiets")
                        .HasForeignKey("idDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_DMCT_DM");

                    b.HasOne("DataBase.Entities.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("idSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_DMCT_SP");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DataBase.Entities.GiaTriThuocTinh", b =>
                {
                    b.HasOne("DataBase.Entities.ThuocTinhChung", null)
                        .WithMany("GiaTriThuocTinhs")
                        .HasForeignKey("IDThuocTinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TT_GTTT");
                });

            modelBuilder.Entity("DataBase.Entities.GioHangChiTiet", b =>
                {
                    b.HasOne("DataBase.Entities.SanPham", "sanPham")
                        .WithMany()
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GHCT_SP");

                    b.HasOne("DataBase.Entities.User", null)
                        .WithMany("GioHangChiTiets")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GHCT_GH");

                    b.Navigation("sanPham");
                });

            modelBuilder.Entity("DataBase.Entities.HoaDon", b =>
                {
                    b.HasOne("DataBase.Entities.User", null)
                        .WithMany("HoaDons")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_HD_KH");
                });

            modelBuilder.Entity("DataBase.Entities.HoaDonChiTiet", b =>
                {
                    b.HasOne("DataBase.Entities.HoaDon", null)
                        .WithMany("HoaDonChiTiets")
                        .HasForeignKey("IDSHoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_HDCT");

                    b.HasOne("DataBase.Entities.SanPham", "sanPham")
                        .WithMany()
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_HDCT_SP");

                    b.Navigation("sanPham");
                });

            modelBuilder.Entity("DataBase.Entities.SanPham", b =>
                {
                    b.HasOne("DataBase.Entities.ItemImage", "anhs")
                        .WithOne()
                        .HasForeignKey("DataBase.Entities.SanPham", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_IMG_SP");

                    b.Navigation("anhs");
                });

            modelBuilder.Entity("DataBase.Entities.ThuocTinh", b =>
                {
                    b.HasOne("DataBase.Entities.ThuocTinhChung", "thuocTinhChung")
                        .WithOne()
                        .HasForeignKey("DataBase.Entities.ThuocTinh", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TTC_TT");

                    b.HasOne("DataBase.Entities.SanPham", null)
                        .WithMany("thuocTinhs")
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TT_SP");

                    b.Navigation("thuocTinhChung");
                });

            modelBuilder.Entity("DataBase.Entities.DanhMuc", b =>
                {
                    b.Navigation("DanhMucChiTiets");
                });

            modelBuilder.Entity("DataBase.Entities.HoaDon", b =>
                {
                    b.Navigation("HoaDonChiTiets");
                });

            modelBuilder.Entity("DataBase.Entities.SanPham", b =>
                {
                    b.Navigation("thuocTinhs");
                });

            modelBuilder.Entity("DataBase.Entities.ThuocTinhChung", b =>
                {
                    b.Navigation("GiaTriThuocTinhs");
                });

            modelBuilder.Entity("DataBase.Entities.User", b =>
                {
                    b.Navigation("GioHangChiTiets");

                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
