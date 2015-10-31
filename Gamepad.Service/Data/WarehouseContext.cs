using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Data
{
    internal class WarehouseContext<TContext>
        where TContext : DbContext, new()
    {
        public WarehouseContext()
        {
            _context = new TContext();
        }

        public WarehouseContext(TContext context)
        {
            _context = context;
        }


        private readonly TContext _context;


        public OperationResult OperationResult { get; private set; }

        public void Create<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Added;
        }

        public void Create<TEntity>(TEntity[] items) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(items);
        }

        public void BatchInsert<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(items);
        }

        public void Delete<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public void Delete<TEntity>(TEntity[] items) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(items);
        }

        public void BatchDelete<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(items);
        }

        public void Update<TEntity>(TEntity item) where TEntity : class
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Update<TEntity>(TEntity[] items) where TEntity : class
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public void BatchUpdate<TEntity>(IQueryable<TEntity> items) where TEntity : class
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
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

        public async Task<OperationResult> SaveChangesAsync()
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
                    OperationResult = OperationResult.Failed(exception.Message);
                }
                catch (Exception exception)
                {
                    OperationResult = OperationResult.Failed(exception.Message);
                }
            });
            await task;
            return OperationResult;
        }

        public OperationResult SaveChanges()
        {
            OperationResult = OperationResult.Success;
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                OperationResult = OperationResult.Failed(exception.Message);
            }
            catch (Exception exception)
            {
                OperationResult = OperationResult.Failed(exception.Message);
            }
            return OperationResult;
        }

    }
}
