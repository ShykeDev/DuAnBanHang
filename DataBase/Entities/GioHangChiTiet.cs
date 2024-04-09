using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class GioHangChiTiet
    {
        public GioHangChiTiet()
        {
        }

        public GioHangChiTiet(Guid iD, Guid iDSanPham, Guid? userID, int soLuong, string thuocTinh)
        {
            ID = iD;
            IDSanPham = iDSanPham;
            UserID = userID;
            SoLuong = soLuong;
            ThuocTinh = thuocTinh;
        }

        public Guid ID { get; set; }
        public Guid IDSanPham { get; set; }
        public Guid? UserID { get; set; }
        public int SoLuong { get; set; }

        public string? ThuocTinh { get; set; }

        public virtual SanPham sanPham { get; set; }
    }
}
