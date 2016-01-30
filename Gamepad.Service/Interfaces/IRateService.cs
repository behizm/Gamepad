using System.Collections.Generic;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IRateService : IBaseService<IRateService, Rate>
    {
        OperationResult Update(Rate item, ICollection<RateContent> rateContents);
    }
}