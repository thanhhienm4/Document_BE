using System.ComponentModel.DataAnnotations;
using Document.Data.EF;
using Document.RestService.Repositories;
using Document.RestService.Repositories.Impl;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Document.RestService.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields

        private ITableRepository _tableRepository = null!;

        #endregion
        #region Properties

        public ITableRepository TableRepository
        {
            get { return _tableRepository ??= new TableRepository(context: _context); }
        }

        #endregion
        #region Methods

        //Here TContext is nothing but your DBContext class
        //In our example it is EmployeeDBContext class
        private readonly DocumentContext _context;
        private bool _disposed;
        private IDbContextTransaction _objTran = null!;
        //Using the Constructor we are initializing the _context variable is nothing but
        //we are storing the DBContext (EmployeeDBContext) object in _context variable
        public UnitOfWork(DocumentContext documentContext)
        {
            _context = documentContext;
        }
        //The Dispose() method is used to free unmanaged resources like files, 
        //database connections etc. at any time.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //This CreateTransaction() method will create a database Trnasaction so that we can do database operations by
        //applying do evrything and do nothing principle
        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }
        //If all the Transactions are completed successfuly then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            _objTran.Commit();
        }
        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }
        //This Save() Method Implement DbContext Class SaveChanges method so whenever we do a transaction we need to
        //call this Save() method so that it will make the changes in the database
        public int Save()
        {
            var entities = from entry in _context.ChangeTracker.Entries()
                           where entry.State == EntityState.Modified || entry.State == EntityState.Added
                           select entry.Entity;

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    // throw new ValidationException() or do whatever you want
                }
            }
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        #endregion

    }
}
