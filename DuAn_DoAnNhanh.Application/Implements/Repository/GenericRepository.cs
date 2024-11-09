using Microsoft.EntityFrameworkCore;
using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyDBContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(MyDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
