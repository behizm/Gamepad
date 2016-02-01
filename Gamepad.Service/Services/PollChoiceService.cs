using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;

namespace Gamepad.Service.Services
{
    internal class PollChoiceService : BaseService<PollChoice>, IPollChoiceService
    {
        public PollChoiceService(GamepadContext context) : base(context)
        {
        }

        public IPollChoiceService Clone()
        {
            return new PollChoiceService(new GamepadContext());
        }
    }
}