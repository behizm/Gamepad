using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface ISystemHardwareService : IBaseService<ISystemHardwareService, SystemHardware>
    {
        SystemHardware FindByName(string name);

        Cluster<SystemHardware> Search(SystemHardwareSearchModel model,
            Ordering<SystemHardware, SystemHardwareType> ordering);
    }
}
