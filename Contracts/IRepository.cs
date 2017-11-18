using System;
using System.Linq;
using System.Linq.Expressions;

public interface IRepository<T> where T : class {
    
    IQueryable<T> GetAll();
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
}