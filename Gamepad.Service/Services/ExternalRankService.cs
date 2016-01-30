using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Services;

namespace Gamepad.Service.Services
{
    internal class ExternalRankService : BaseService<ExternalRank>, IExternalRankService
    {
        public ExternalRankService(GamepadContext context) : base(context)
        {
        }

        public IExternalRankService Clone()
        {
            return new ExternalRankService(new GamepadContext());
        }
    }
}