using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class ThuocTinh
    {
        public ThuocTinh()
        {
        }

        public ThuocTinh(Guid iDThuocTinhChung, Guid iDSanPham)
        {
            ID = iDThuocTinhChung;
            IDSanPham = iDSanPham;
        }

        public Guid ID { get; set; }
        public Guid IDSanPham { get; set; }
        public virtual ThuocTinhChung thuocTinhChung { get; set; }
    }
}
