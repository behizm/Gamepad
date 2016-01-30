using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class SystemRequirmentService : BaseService<SystemRequirment>, ISystemRequirmentService
    {
        public SystemRequirmentService(GamepadContext context) : base(context)
        {
        }

        public ISystemRequirmentService Clone()
        {
            return new SystemRequirmentService(new GamepadContext());
        }

        public override OperationResult Insert(SystemRequirment item)
        {
            var req =
                Get(
                    x =>
                        x.ArticleId == item.ArticleId && x.SystemHardwareId == item.SystemHardwareId &&
                        x.RequirmentType == item.RequirmentType);
            return req != null ? OperationResult.Failed(ErrorMessages.Services_General_Duplicate) : base.Insert(item);
        }

        public override OperationResult Update(SystemRequirment item)
        {
            var req =
                Get(
                    x =>
                        x.ArticleId == item.ArticleId && x.SystemHardwareId == item.SystemHardwareId &&
                        x.RequirmentType == item.RequirmentType);
            if (req.Id != item.Id)
            {
                OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            return base.Update(item);
        }
    }
}