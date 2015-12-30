using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Repositories
{
    internal class WarehouseContext<TContext> where TContext : DbContext, new()
    {

        private readonly TContext _context;

        public WarehouseContext()
        {
            _context = new TContext();
        }

        public WarehouseContext(TContext context)
        {
            _context = context;
        }



        public OperationResult OperationResult { get; private set; }

        public void Insert<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            _context.Entry(item).State = EntityState.Added;
        }

        public void InsertRange<TEntity>(ICollection<TEntity> items) where TEntity : BaseEntity
        {
            _context.Set<TEntity>().AddRange(items);
        }


        public void Update<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void UpdateBatch<TEntity>(ICollection<TEntity> items) where TEntity : BaseEntity
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }


        public void Delete<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public void DeleteRange<TEntity>(ICollection<TEntity> items) where TEntity : BaseEntity
        {
            _context.Set<TEntity>().RemoveRange(items);
        }


        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
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

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            OperationResult = OperationResult.Success;
            try
            {
                return _context.Set<TEntity>().Single(predicate);
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public async Task<IQueryable<TEntity>> SearchAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
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

        public IQueryable<TEntity> Search<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            OperationResult = OperationResult.Success;
            try
            {
                return _context.Set<TEntity>().Where(predicate);
            }
            catch (Exception ex)
            {
                OperationResult = OperationResult.Failed(ex.Message);
                return null;
            }
        }

        public async Task<int?> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
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

        public int? Count<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            OperationResult = OperationResult.Success;
            try
            {
                return _context.Set<TEntity>().Count(predicate);
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
