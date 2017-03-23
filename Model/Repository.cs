using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Model
{
    public class Repository : IEntityFrameworkRepository
    {
        protected DbContext Context;

        public Repository(DbContext context, bool autoDetectChangesEnabled = false, bool proxyCreationEnabled = false)
        {
            Context = context;
            Context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;

        }

        #region IDisposable Support
        private bool _disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                        Context = null;
                    }
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                _disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~Repository() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public T Create<T>(T entity) where T : class
        {
            T result = Context.Set<T>().Add(entity);
            TrySaveChanges();

            return result;
        }

        protected int TrySaveChanges()
        {
            return Context.SaveChanges();
        }

        public bool Update<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return TrySaveChanges() == 1;
        }

        public bool Delete<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            return TrySaveChanges() == 1;
        }

        public bool Delete<T>(Expression<Func<T,bool>> criteria ) where T : class
        {
            T entityToDelete = Context.Set<T>().FirstOrDefault(criteria);
            if (entityToDelete == null) throw new InvalidOperationException("No se encontró ningun registro para eliminar.");
            Context.Set<T>().Attach(entityToDelete);
            Context.Entry(entityToDelete).State = EntityState.Deleted;
            return TrySaveChanges() == 1;
        }

        public bool DeleteSet<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            IEnumerable<T> entitiesToDelete = Context.Set<T>().Where(criteria);
            if (entitiesToDelete == null) throw new InvalidOperationException("No se encontró ningun registro para eliminar.");

            foreach (T entity in entitiesToDelete)
            {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Deleted;
            }

            return TrySaveChanges() == entitiesToDelete.Count();
        }


        public bool Exists<T>(Expression<Func<T, bool>> criteria) where T:class 
        {
            return Context.Set<T>().Any(criteria);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return Context.Set<T>();
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            return Context.Set<T>().Where(criteria);
        }

        public T GetEntity<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            return Context.Set<T>().FirstOrDefault(criteria);
        }

        public IEnumerable<T> GetAll<T>(params string[] loadPropertyName) where T : class
        {
            return LoadProperties<T>(loadPropertyName);
        }

        private DbQuery<T> LoadProperties<T>(string[] loadPropertyName) where T : class
        {
            DbQuery<T> query = Context.Set<T>();

            foreach (string property in loadPropertyName)
            {
                query = query.Include(property);
            }
            return query;
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> criteria, params string[] loadPropertyName) where T : class
        {
            return LoadProperties<T>(loadPropertyName).Where(criteria);
        }

        public T GetEntity<T>(Expression<Func<T, bool>> criteria, params string[] loadPropertyName) where T : class
        {
            return LoadProperties<T>(loadPropertyName).FirstOrDefault(criteria);
        }
    }
}