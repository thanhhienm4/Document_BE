using Document.Data.EF.Entities;

namespace Document.RestService.Services
{
    public interface ITableService
    {
        IQueryable<Table> GetAll();
    }
}
