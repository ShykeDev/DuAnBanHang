using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class HoaDonChiTiet
    {
        public HoaDonChiTiet()
        {
        }

        public HoaDonChiTiet(Guid iD, Guid iDSanPham, int giaSanPham, int soLuong, string? thuocTinh)
        {
            ID = iD;
            IDSanPham = iDSanPham;
            GiaSanPham = giaSanPham;
            SoLuong = soLuong;
            ThuocTinh = thuocTinh;
        }

        public Guid ID { get; set; }
        public Guid IDSanPham { get; set; }
        public int GiaSanPham { get; set; }
        public int SoLuong { get; set; }
        public string? ThuocTinh { get; set; }

        public virtual SanPham sanPham { get; set; }
    }
}
