using Document.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Document.RestService.Repositories.Impl
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class

    {
        private DbSet<T> _entities = null!;
        public bool IsDisposed { get; private set; }

        public GenericRepository(DocumentContext context)
        {
            IsDisposed = false;
            Context = context;
        }

        protected DocumentContext Context { get; set; }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= Context.Set<T>(); }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            IsDisposed = true;
        }

        public virtual IQueryable <T> GetAll()
        {
            return Entities;
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id)!;
        }

        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Entities.Add(entity);
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            Context.Set<T>().AddRange(entities);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            SetEntryModified(entity);
            //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
            //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
        }

        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}