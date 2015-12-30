using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IFileService : IBaseService<IFileService, File>
    {
        OperationResult Insert(File item, string username);
    }
}
