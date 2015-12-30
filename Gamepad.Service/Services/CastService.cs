using System;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class CastService : BaseService<Cast>, ICastService
    {
        public CastService(GamepadContext context) : base(context)
        { }

        public ICastService Clone()
        {
            return new CastService(new GamepadContext());
        }

        public override OperationResult Insert(Cast item)
        {
            var cast =
                Get(x =>
                    x.CastType == item.CastType &&
                    (x.Value.ToLower() == item.Value.ToLower() ||
                     x.FaValue.ToLower() == item.FaValue.ToLower()));

            if (cast != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Insert(item);
        }

        public Cast FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Value.ToLower() == name || x.FaValue.ToLower() == name);
        }

        public override OperationResult Update(Cast item)
        {
            var cast = FindById(item.Id);
            if (cast == null)
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);

            var exictedCast = Get(
                x => x.Id != cast.Id &&
                     x.CastType == item.CastType &&
                     (x.Value.ToLower() == item.Value.ToLower() ||
                      x.FaValue.ToLower() == item.FaValue.ToLower()));

            if (exictedCast != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            cast.FaValue = item.FaValue;
            cast.Value = item.Value;
            cast.CastType = item.CastType;
            return base.Update(cast);
        }

        public Cluster<Cast> Search<TOrderingKey>(CastSearchModel model, Ordering<Cast, TOrderingKey> ordering)
        {
            Expression<Func<Cast, bool>> expression;
            if (model == null)
            {
                expression = u => true;
            }
            else
            {
                expression = u =>
                    (!model.CastType.HasValue || u.CastType == model.CastType) &&
                    (string.IsNullOrEmpty(model.Value) || u.Value.ToLower().Contains(model.Value)) &&
                    (string.IsNullOrEmpty(model.FaValue) || u.FaValue.ToLower().Contains(model.FaValue));
            }
            return Search(expression, ordering);
        }
    }
}
