using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class RateService : BaseService<Rate>, IRateService
    {
        public RateService(GamepadContext context) : base(context)
        {
        }

        public IRateService Clone()
        {
            return new RateService(new GamepadContext());
        }

        public override OperationResult Insert(Rate item)
        {
            var rate = Get(x => x.ArticleId == item.ArticleId && x.RateSource == item.RateSource);
            return rate != null ? OperationResult.Failed(ErrorMessages.Services_General_Duplicate) : base.Insert(item);
        }

        public override OperationResult Update(Rate item)
        {
            var rate = Get(x => x.ArticleId == item.ArticleId && x.RateSource == item.RateSource);
            return rate.Id != item.Id ? OperationResult.Failed(ErrorMessages.Services_General_Duplicate) : base.Update(rate);
        }

        public OperationResult Update(Rate item, ICollection<RateContent> rateContents)
        {
            var rate = Get(x => x.ArticleId == item.ArticleId && x.RateSource == item.RateSource);
            if (rate.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            var newRateContents =
                rateContents.Where(
                    x => !rate.RateContents.Any(r => r.RateSource == x.RateSource && r.Content == x.Content)).ToList();
            foreach (var newRateContent in newRateContents)
            {
                rate.RateContents.Add(newRateContent);
            }
            var deletedRateContents =
                rate.RateContents.Where(
                    x => !rateContents.Any(r => r.RateSource == x.RateSource && r.Content == x.Content)).ToList();
            foreach (var deletedRateContent in deletedRateContents)
            {
                Context.Entry(deletedRateContent).State = EntityState.Deleted;
            }
            return base.Update(rate);
        }

        public override OperationResult Delete(Guid id)
        {
            var rate = FindById(id);
            return rate == null ? OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound) : Delete(rate);
        }

        public override OperationResult Delete(Rate item)
        {
            var rateContents = item.RateContents.ToList();
            foreach (var rateContent in rateContents)
            {
                Context.Entry(rateContent).State = EntityState.Deleted;
            }
            return base.Delete(item);
        }

        public override OperationResult Delete(Expression<Func<Rate, bool>> predicate)
        {
            var rate = Get(predicate);
            return rate == null ? OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound) : Delete(rate);
        }

    }
}