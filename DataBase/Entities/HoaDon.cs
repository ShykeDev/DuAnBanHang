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
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime NgayMua { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public TrangThaiDonHang TrangThaiDonHang { get; set; }

        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    }
}
