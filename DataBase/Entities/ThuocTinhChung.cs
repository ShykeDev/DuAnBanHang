using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class ThuocTinhChung
    {
        public Guid ID { get; set; }
        public string TenThuocTinh { get; set; }
        public virtual ICollection<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }
    }
}
