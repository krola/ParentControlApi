using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class Repository<T> : 
    IRepository<T> where T : class {

    private DbContext _entities = new ParentControlContext();
    public DbContext Context {

        get { return _entities; }
        set { _entities = value; }
    }

    public virtual IQueryable<T> GetAll() {

        IQueryable<T> query = _entities.Set<T>();
        return query;
    }

    public virtual T Get(int id) {
        T query = _entities.Set<T>().Find(id);
        return query;
    }

    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate) {

        IQueryable<T> query = _entities.Set<T>().Where(predicate);
        return query;
    }

    public void Add(T entity) {
        _entities.Set<T>().Add(entity);
        _entities.SaveChanges();
    }

    public void Delete(T entity) {
        _entities.Set<T>().Remove(entity);
        _entities.SaveChanges();
    }

    public void Edit(T entity) {
        _entities.Entry(entity).State = EntityState.Modified;
        _entities.SaveChanges();
    }
}