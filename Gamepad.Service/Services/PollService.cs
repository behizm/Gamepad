using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Services
{
    internal class PollService : BaseService<Poll>, IPollService
    {
        public PollService(GamepadContext context) : base(context)
        {
        }

        public IPollService Clone()
        {
            return new PollService(new GamepadContext());
        }

    }
}