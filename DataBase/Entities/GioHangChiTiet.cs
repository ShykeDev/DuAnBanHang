﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class GioHangChiTiet
    {
        public Guid ID { get; set; }
        public Guid IDSanPham { get; set; }
        public int SoLuong { get; set; }

        public virtual GioHang gioHang { get; set; }
        public virtual SanPham sanPham { get; set; }
    }
}
