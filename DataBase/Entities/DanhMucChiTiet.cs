using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class DanhMucChiTiet
    {
        public Guid idDanhMuc { get; set; }
        public Guid idSanPham { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
