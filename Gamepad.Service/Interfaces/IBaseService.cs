﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IBaseService<out TInterface, TEntity> where TEntity : BaseEntity
    {
        TInterface Clone();

        OperationResult Insert(TEntity item);
        OperationResult InsertRange(ICollection<TEntity> items);
        OperationResult Update(TEntity item);
        OperationResult UpdateBatch(ICollection<TEntity> items);
        OperationResult Delete(TEntity item);
        OperationResult Delete(Guid id);
        OperationResult DeleteRange(ICollection<TEntity> items);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(Guid id);
        IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        int? Count(Expression<Func<TEntity, bool>> predicate);

        OperationResult SaveChanges();
    }
}
