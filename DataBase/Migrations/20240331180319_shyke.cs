using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class shyke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinhChungs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThuocTinh = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhChungs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    GiaGoc = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GiaGiamGia = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IMG_SP",
                        column: x => x.ID,
                        principalTable: "ItemImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaTriThuocTinhs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDThuocTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiaTri = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTriThuocTinhs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TT_GTTT",
                        column: x => x.IDThuocTinh,
                        principalTable: "ThuocTinhChungs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GH_KH",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThaiDonHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HD_KH",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinhs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhs", x => new { x.ID, x.IDSanPham });
                    table.ForeignKey(
                        name: "FK_TTC_TT",
                        column: x => x.ID,
                        principalTable: "ThuocTinhChungs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TT_SP",
                        column: x => x.IDSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiets", x => new { x.ID, x.IDSanPham });
                    table.ForeignKey(
                        name: "FK_GHCT_GH",
                        column: x => x.ID,
                        principalTable: "GioHangs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GHCT_SP",
                        column: x => x.IDSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiaSanPham = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiets", x => new { x.ID, x.IDSanPham });
                    table.ForeignKey(
                        name: "FK_HDCT",
                        column: x => x.ID,
                        principalTable: "HoaDons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HDCT_SP",
                        column: x => x.IDSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "DiaChi", "Email", "Name", "NgaySinh", "Password", "Role", "SDT", "State", "UserName" },
                values: new object[] { new Guid("37665620-5d8b-431d-bd16-71efd80a57e6"), "Phúc Diễn, Bắc Từ Liêm, Hà Nội", "nhatvu@gmail.com", "Nguyễn Lê Nhất Vũ", "2004-01-01", "19112004", 1, "0865805582", 0, "shyke" });

            migrationBuilder.CreateIndex(
                name: "IX_GiaTriThuocTinhs_IDThuocTinh",
                table: "GiaTriThuocTinhs",
                column: "IDThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_IDSanPham",
                table: "GioHangChiTiets",
                column: "IDSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_IDSanPham",
                table: "HoaDonChiTiets",
                column: "IDSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_UserID",
                table: "HoaDons",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhs_ID",
                table: "ThuocTinhs",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhs_IDSanPham",
                table: "ThuocTinhs",
                column: "IDSanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaTriThuocTinhs");

            migrationBuilder.DropTable(
                name: "GioHangChiTiets");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiets");

            migrationBuilder.DropTable(
                name: "ThuocTinhs");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "ThuocTinhChungs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ItemImages");
        }
    }
}
