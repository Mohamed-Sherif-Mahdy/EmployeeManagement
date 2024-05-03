using System.Linq.Expressions;

namespace EmployeeManagement_API.Repository
{
    public interface IRepository<T>
        where T : class
    {
        List<T> GetAll();
        T GetBy(Expression<Func<T, bool>> expression);

        List<T> Get(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Update(T entity);
        void Update(T OldEntity, T NewEntity);
        void Delete(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        List<T> GetWithInclude(Expression<Func<T, object>> include);

        List<T> GetWithInclude(string include);
    }
}
