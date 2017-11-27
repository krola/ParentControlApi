using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public abstract class GenericRepository<C, T> : 
    IRepository<T> where T : class where C : DbContext, new() {

    private C _entities = new C();
    public C Context {

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

    public virtual void Add(T entity) {
        _entities.Set<T>().Add(entity);
    }

    public virtual void Delete(T entity) {
        _entities.Set<T>().Remove(entity);
    }

    public virtual void Edit(T entity) {
        //_entities.Entry(entity).State = System.Data.;
    }
}