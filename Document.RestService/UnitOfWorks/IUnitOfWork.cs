using Document.RestService.Repositories;

namespace Document.RestService.UnitOfWorks
{
    public interface IUnitOfWork
    {
        #region methods
        void CreateTransaction();
        void Commit();
        void Rollback();
        int Save();

        #endregion methods

        #region Properties

        ITableRepository TableRepository { get; }

        #endregion
    }
}
