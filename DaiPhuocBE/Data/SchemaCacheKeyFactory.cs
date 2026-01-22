using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DaiPhuocBE.Data
{
    public class SchemaCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
        {
            if (context is MasterDbContext dynamicContext)
            {
                return (context.GetType(), dynamicContext.TransactionSchema, designTime);
            }

            return (context.GetType(), designTime);
        }
    }
}
