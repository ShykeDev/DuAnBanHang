using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IAllRepository<T> where T : class
    {
        public ICollection<T> GetAll();
        public T GetByID(dynamic id);
        public bool CreateObj(T obj);
        public bool UpdateObj(T obj);
        public bool DeleteObj(T obj);

    }
}
