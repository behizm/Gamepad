using System.Threading.Tasks;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Repositories
{
    internal class FileService : BaseService, IFileService
    {
        public IFileService Clone()
        {
            return new FileService();
        }

        public async Task<OperationResult> AddFileAsync(FileAddModel model)
        {
            if (!ValidateModel(model))
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);

            var user = await RepositoryContext.RetrieveAsync<User>(u => u.Username == model.Username);
            if (user == null)
                return OperationResult.Failed(ErrorMessages.Services_User_UserNotFound);

            var file = new File
            {
                Category = model.Category,
                FileType = model.FileType,
                Address = model.Address,
                CreatorId = user.Id,
                Filename = model.Filename,
                IsPublic = model.IsPublic,
                Size = model.Size,
                Title = model.Title
            };
            await RepositoryContext.CreateAsync(file);
            return RepositoryContext.OperationResult;
        }
    }
}
