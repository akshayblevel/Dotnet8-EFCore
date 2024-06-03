using Dotnet8_EFCore.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dotnet8_EFCore
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext DataContext;

        public Repository(DbContext context)
        {
            DataContext = context;
            DataContext.Database.SetCommandTimeout(180);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var newEntity = await DataContext.Set<T>().AddAsync(entity);
            await SaveAsync();
            return newEntity.Entity;
        }

        public DbSet<T> DbSet()
        {
            return DataContext.Set<T>();
        }

        public async Task DeleteAsync(T entity)
        {
            DataContext.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where, IEnumerable<string> includes = null)
        {
            var queryable = DataContext.Set<T>().AsQueryable();
            if(includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, includes) => current.Include(includes));
            }
            var entity = await queryable.Where(where).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            var queryable = DataContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, includes) => current.Include(includes));
            }
            return await queryable.Where(where).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsyncWithQuery(Expression<Func<IQueryable<T>, IQueryable<T>>> conditions)
        {
            var queryable = DataContext.Set<T>().AsQueryable();
            var compiledConditions = conditions.Compile();
            queryable = compiledConditions(queryable);
            return await queryable.ToListAsync();
        }

        public async Task<bool> SaveAsync(bool acceptAllChanges = true)
        {
            return await DataContext.SaveChangesAsync(acceptAllChanges) > 0;
        }

        public async Task<List<T>> ToListAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            var queryable = DataContext.Set<T>().AsQueryable();
            if (includes != null)
            {
                queryable = includes.Aggregate(queryable, (current, includes) => current.Include(includes));
            }
            var entities = await queryable.Where(where).ToListAsync();
            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = DataContext.Set<T>().Update(entity);
            await SaveAsync();
            return entry.Entity;
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            DataContext.Set<T>().UpdateRange(entities); 
            return SaveAsync();   
        }

        public async Task BulkInsertAsync(IEnumerable<T> entities, BulkConfig bulkConfig = null)
        {
            using (var transaction = DataContext.Database.BeginTransaction())
            {
                if (bulkConfig != null)
                    await DataContext.BulkInsertAsync(entities, bulkConfig);
                else
                    await DataContext.BulkInsertAsync(entities);
                await transaction.CommitAsync();
            }
        }

        public async Task BulkUpdateAsync(IEnumerable<T> entities, BulkConfig bulkConfig = null)
        {
            using (var transaction = DataContext.Database.BeginTransaction())
            {
                if (bulkConfig != null)
                    await DataContext.BulkUpdateAsync(entities, bulkConfig);
                else
                    await DataContext.BulkUpdateAsync(entities);
                await transaction.CommitAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (DataContext == null)
            {
                return;
            }

            DataContext.Dispose();
            DataContext = null;
        }
    }
}
