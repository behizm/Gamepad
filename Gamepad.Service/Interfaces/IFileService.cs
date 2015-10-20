using System.Threading.Tasks;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;

namespace Gamepad.Service.Interfaces
{
    public interface IFileService : IBaseService<IFileService>
    {
        Task<OperationResult> AddFileAsync(FileAddModel model);
    }
}
