using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Data
{
    internal class RepositoryContext<TContext>
        where TContext : DbContext, new()
    {
        public RepositoryContext()
        {
            _context = new TContext();
        }

        public RepositoryContext(TContext context)
        {
            _context = context;
        }


        private readonly TContext _context;


        public OperationResult OperationResult { get; private set; }

        public async Task CreateAsync<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Added;
            await SaveChanges();
        }

        public async Task CreateAsync<TEntity>(TEntity[] items) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(items);
            await SaveChanges();
        }

        public async Task BatchInsertAsync<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(items);
            await SaveChanges();
        }

        public async Task DeleteAsync<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task DeleteAsync<TEntity>(TEntity[] items) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(items);
            await SaveChanges();
        }

        public async Task BatchDeleteAsync<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(items);
            await SaveChanges();
        }

        public async Task UpdateAsync<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task UpdateAsync<TEntity>(TEntity[] items) where TEntity : class
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            await SaveChanges();
        }

        public async Task BatchUpdateAsync<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            await SaveChanges();
        }

        public async Task<TEntity> RetrieveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            OperationResult = OperationResult.Success;
            try
            {
                return await _context.Set<TEntity>().SingleAsync(predicate);
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public async Task<IQueryable<TEntity>> SearchAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            OperationResult = OperationResult.Success;
            try
            {
                return await Task.Run(() => _context.Set<TEntity>().Where(predicate));
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public async Task<int?> CountAsync<TEntity>() where TEntity : class
        {
            OperationResult = OperationResult.Success;
            try
            {
                return await _context.Set<TEntity>().CountAsync();
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public async Task<int?> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            OperationResult = OperationResult.Success;
            try
            {
                return await _context.Set<TEntity>().CountAsync(predicate);
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public RepositoryContext<TContext> Shadow()
        {
            return new RepositoryContext<TContext>(new TContext());
        }


        private async Task SaveChanges()
        {
            var task = Task.Run(() =>
            {
                OperationResult = OperationResult.Success;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbEntityValidationException exception)
                {
                    OperationResult = OperationResult.Failed(exception.Message, "خطا از سمت پایگاه داده");
                }
                catch (Exception exception)
                {
                    OperationResult = OperationResult.Failed(exception.Message, "خطا از سمت پایگاه داده");
                }
            });
            await task;
        }

    }
}
