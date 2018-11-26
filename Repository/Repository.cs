using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class Repository<T> : 
    IRepository<T> where T : class {

    private DbContext _context;

    public Repository(ParentControlContext context)
    {
        _context = context;
    }

    public DbContext Context {

        get { return _context; }
        set { _context = value; }
    }

    public virtual IQueryable<T> GetAll() {

        IQueryable<T> query = _context.Set<T>();
        return query;
    }

    public virtual T Get(int id) {
        T query = _context.Set<T>().Find(id);
        return query;
    }

    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate) {

        IQueryable<T> query = _context.Set<T>().Where(predicate);
        return query;
    }

    public void Add(T entity) {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity) {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public void Edit(T entity) {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
}