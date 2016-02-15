using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Services
{
    internal class FileService : BaseService<File>, IFileService
    {
        public FileService(GamepadContext context) : base(context)
        {
        }

        public IFileService Clone()
        {
            return new FileService(new GamepadContext());
        }

        public override OperationResult Insert(File item)
        {
            item.EditDate = null;
            return base.Insert(item);
        }

        public OperationResult Insert(File item, string username)
        {
            var user = GpServices.User.FindByUsername(username);
            if (user != null)
            {
                item.CreatorId = user.Id;
            }
            return Insert(item);
        }

        // todo : thumbnails for images or videos
    }
}
