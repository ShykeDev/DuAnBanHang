using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string NgaySinh { get; set; }
        public int Role { get; set; }
        public int State { get; set; }

        public virtual ICollection<HoaDon>? HoaDons { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }

        public User() { }
        public User(Guid ID, string name, string username, string password, string sdt, string email, string diaChi, string ngaySinh, int role)
        {
            this.ID = ID;
            this.Name = name;
            this.UserName = username;
            this.Password = password;
            this.SDT = sdt;
            this.Email = email;
            this.DiaChi = diaChi;
            this.NgaySinh = ngaySinh;
            this.Role = role;
            this.State = 0;
        }
    }
}