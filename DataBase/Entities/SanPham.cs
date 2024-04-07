using DataBase.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class SanPham
    {
        public SanPham() { }
        public SanPham(Guid iD, string name, int giaGoc, int giaGiamGia, int soLuong, int trangThai, string? moTa)
        {
            ID = iD;
            Name = name;
            GiaGoc = giaGoc;
            GiaGiamGia = giaGiamGia;
            SoLuong = soLuong;
            TrangThai = trangThai;
            MoTa = moTa;
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public int GiaGoc { get; set; }
        public int GiaGiamGia { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<ThuocTinh>? thuocTinhs { get; set; }
        public virtual ItemImage anhs { get; set; }

    }
}
