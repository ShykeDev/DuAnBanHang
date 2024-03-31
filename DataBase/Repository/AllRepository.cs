using DataBase.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class AllRepository<T> : IAllRepository<T> where T : class
    {
        MainDbContext _context;
        DbSet<T> _dbSet;
        public AllRepository()
        {
            _context = new MainDbContext();
        }
        public AllRepository(MainDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }
        public bool CreateObj(T obj)
        {
            try
            {
                _dbSet.Add(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteObj(T obj)
        {
            try
            {
                _dbSet.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetByID(dynamic id)
        {
            return _dbSet.Find(id).ToList();
        }

        public bool UpdateObj(T obj)
        {
            try
            {
                _dbSet.Update(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
