using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Confab.Shared.Infrastructure.Postgres
{
    public abstract class PostgresUnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T dbContext;

        public PostgresUnitOfWork(T dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ExcuteAsync(Func<Task> action)
        {
            await using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await action();
                    await transaction.CommitAsync(); 
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
