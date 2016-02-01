using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;

namespace Gamepad.Service.Services
{
    internal class PollUserAnswerServices : BaseService<PollUserAnswer>, IPollUserAnswerService
    {
        public PollUserAnswerServices(GamepadContext context) : base(context)
        {
        }

        public IPollUserAnswerService Clone()
        {
            return new PollUserAnswerServices(new GamepadContext());
        }
    }
}