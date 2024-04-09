using DataBase.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class HoaDon
    {
        public HoaDon()
        {
        }

        public HoaDon(Guid iD, Guid? userID, string nameuser, DateTime ngayMua, string sDT, string email, string diaChi, string? chuThich, int trangThaiDonHang)
        {
            ID = iD;
            UserID = userID;
            nameUser = nameuser;
            NgayMua = ngayMua;
            SDT = sDT;
            Email = email;
            DiaChi = diaChi;
            ChuThich = chuThich;
            TrangThaiDonHang = trangThaiDonHang;
        }

        public Guid ID { get; set; }
        public Guid? UserID { get; set; }
        public string nameUser { get; set; }
        public DateTime NgayMua { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string? ChuThich { get; set; }
        public int TrangThaiDonHang { get; set; }

        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    }
}
