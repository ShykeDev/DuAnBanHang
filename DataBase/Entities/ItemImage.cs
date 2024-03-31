using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class ItemImage
    {
        public Guid ID { get; set; }
        public string Path { get; set; }
        public virtual SanPham sanPhams { get; set; }
    }
}
