using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IRepository<T> where T : class {
    
    IEnumerable<T> GetAll();
    T Get(int id);
    IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

    void Add(T entity);

    void Delete(T entity);
    void Edit(T entity);
}