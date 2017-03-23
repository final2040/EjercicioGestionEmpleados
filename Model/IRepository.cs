using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model
{
    public interface IRepository : IDisposable
    {
        T Create<T>(T entity) where T : class;
        bool Update<T>(T entity) where T : class;
        bool Delete<T>(T entity) where T : class;
        bool Exists<T>(Expression<Func<T, bool>> criteria) where T:class ;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria) where T : class;
        T GetEntity<T>(Expression<Func<T, bool>> criteria) where T : class;
        bool DeleteSet<T>(Expression<Func<T, bool>> criteria) where T : class;
        bool Delete<T>(Expression<Func<T, bool>> criteria) where T : class;
    }
}