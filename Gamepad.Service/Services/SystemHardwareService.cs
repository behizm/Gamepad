using System;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class SystemHardwareService : BaseService<SystemHardware>, ISystemHardwareService
    {
        public SystemHardwareService(GamepadContext context) : base(context)
        {
        }

        public ISystemHardwareService Clone()
        {
            return new SystemHardwareService(new GamepadContext());
        }

        public SystemHardware FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name.ToLower() == name);
        }

        public override OperationResult Insert(SystemHardware item)
        {
            var systemHardware = Get(x => x.HardwareType == item.HardwareType && x.Name.ToLower() == item.Name.ToLower());
            if (systemHardware != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Insert(item);
        }

        public override OperationResult Update(SystemHardware item)
        {
            var systemHardware =
                Get(
                    x =>
                        x.Id != item.Id && x.HardwareType == item.HardwareType &&
                        x.Name.ToLower() == item.Name.ToLower());
            if (systemHardware != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Update(item);
        }

        public Cluster<SystemHardware> Search(SystemHardwareSearchModel model, Ordering<SystemHardware, SystemHardwareType> ordering)
        {
            Expression<Func<SystemHardware, bool>> expression;
            if (model == null)
            {
                expression = u => true;
            }
            else
            {
                expression = u =>
                    (!model.HardwareType.HasValue || u.HardwareType == model.HardwareType) &&
                    (string.IsNullOrEmpty(model.Name) || u.Name.ToLower().Contains(model.Name));
            }
            return Search(expression, ordering);
        }


    }
}
