using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Services
{
    internal class CommunityService : BaseService<Community>, ICommunityService
    {
        public CommunityService(GamepadContext context) : base(context)
        {
        }

        public ICommunityService Clone()
        {
            return new CommunityService(new GamepadContext());
        }


        public override OperationResult Insert(Community item)
        {
            item.Body = SwearWordFilter(item.Body);
            return base.Insert(item);
        }

        public override OperationResult Update(Community item)
        {
            item.Body = SwearWordFilter(item.Body);
            return base.Update(item);
        }


    }
}