using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dotnet8_EFCore.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<bool> SaveAsync(bool acceptAllChanges = true);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        Task<IEnumerable<T>> GetAsyncWithQuery(Expression<Func<IQueryable<T>, IQueryable<T>>> conditions);
        Task<T> UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task BulkInsertAsync(IEnumerable<T> entities, BulkConfig bulkConfig = null);
        Task BulkUpdateAsync(IEnumerable<T> entities, BulkConfig bulkConfig = null);
        DbSet<T> DbSet();
    }
}
