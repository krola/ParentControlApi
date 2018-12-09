using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

public class Repository<T> : 
    IRepository<T> where T : class {
    private readonly IConfiguration configuration;

    public Repository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public virtual IEnumerable<T> GetAll() {
        using(var context = new ParentControlContext(configuration))
        {
            IQueryable<T> query = context.Set<T>();
            return query;
        }
    }

    public virtual T Get(int id) {
        using(var context = new ParentControlContext(configuration))
        {
            T query = context.Set<T>().Find(id);
            return query;
        }
    }

    public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate) {
        using(var context = new ParentControlContext(configuration))
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);
            return query.ToList();
        }
    }

    public void Add(T entity) {
        using(var context = new ParentControlContext(configuration))
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
    }

    public void Delete(T entity) {
        using(var context = new ParentControlContext(configuration))
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
    }

    public void Edit(T entity) {
        using(var context = new ParentControlContext(configuration))
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}