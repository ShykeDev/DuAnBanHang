using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class DanhMucChiTiet
    {
        public DanhMucChiTiet()
        {
        }

        public DanhMucChiTiet(Guid idDanhMuc, Guid idSanPham)
        {
            this.idDanhMuc = idDanhMuc;
            this.idSanPham = idSanPham;
        }

        public Guid idDanhMuc { get; set; }
        public Guid idSanPham { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
