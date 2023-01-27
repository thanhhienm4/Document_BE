using Document.Data.EF;
using Document.Data.EF.Entities;
using Document.RestService.UnitOfWorks;

namespace Document.RestService.Services.Impl
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Table> GetAll()
        {
            return _unitOfWork.TableRepository.GetAll();
        }
    }
}
