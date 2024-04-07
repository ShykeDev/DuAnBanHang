using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class GiaTriThuocTinh
    {
        public GiaTriThuocTinh()
        {
        }

        public GiaTriThuocTinh(Guid iD, Guid iDThuocTinh, string giaTri)
        {
            ID = iD;
            IDThuocTinh = iDThuocTinh;
            GiaTri = giaTri;
        }

        public Guid ID { get; set; }
        public Guid IDThuocTinh { get; set; }
        public string GiaTri { get; set; }
    }
}
