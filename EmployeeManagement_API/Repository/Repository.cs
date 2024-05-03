
using EmployeeManagement_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement_API.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly EmployeeManagemendtDbContext _context;
    public Repository(EmployeeManagemendtDbContext context)
    {
      _context = context;
    }
    public void Delete(T entity)
    {

      _context.Set<T>().Remove(entity);
      _context.SaveChanges();
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
      return _context.Set<T>().Any(expression);
    }

    public List<T> Get(Expression<Func<T, bool>> expression)
    {
      return _context.Set<T>().Where(expression).ToList();
    }

    public List<T> GetAll()
    {
      return _context.Set<T>().ToList();
    }

    public T GetBy(Expression<Func<T, bool>> expression)
    {
      return _context.Set<T>().FirstOrDefault(expression);
    }

    public List<T> GetWithInclude(Expression<Func<T, object>> include)
    {
      return _context.Set<T>().Include(include).ToList();
    }

    public void Insert(T entity)
    {
      _context.Set<T>().Add(entity);
      _context.SaveChanges();
    }

    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
      _context.SaveChanges();
    }

    public void Update(T OldEntity, T NewEntity)
    {
      _context.Entry(OldEntity).CurrentValues.SetValues(NewEntity);
      _context.SaveChanges();
    }
  }
}
