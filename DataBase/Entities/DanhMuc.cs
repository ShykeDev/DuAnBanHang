using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class DanhMuc
    {
        public DanhMuc()
        {
        }

        public DanhMuc(Guid iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public Guid ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DanhMucChiTiet>? DanhMucChiTiets { get; set; }
    }
}
