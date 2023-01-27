using System.Reflection.Metadata;
using Document.Data.EF;
using Document.Data.EF.Entities;

namespace Document.RestService.Repositories.Impl
{
    public class TableRepository: GenericRepository<Table>, ITableRepository
    {
        public TableRepository(DocumentContext context) : base(context)
        {
            
        }
    }
}
