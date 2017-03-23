using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model
{
    public interface IEntityFrameworkRepository:IRepository
    {
        IEnumerable<T> GetAll<T>(params string[] loadPropertyName) where T : class;
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria, params string[] loadPropertyName) where T : class;
        T GetEntity<T>(Expression<Func<T, bool>> criteria, string[] loadPropertyName) where T : class;
    }
}