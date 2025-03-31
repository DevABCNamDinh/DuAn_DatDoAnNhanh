using System.Linq.Expressions;

namespace DuAn_DoAnNhanh.Data.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        T GetById(Guid id);
        Task<T> GetByIdAsync(int id);
       
        void Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);


        void Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void SaveChanges();
        Task SaveChangesAsync();

        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria);


    }
}
