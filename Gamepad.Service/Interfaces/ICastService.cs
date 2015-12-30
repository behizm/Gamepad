using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.CrossModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface ICastService : IBaseService<ICastService, Cast>
    {
        Cast FindByName(string name);
        Cluster<Cast> Search<TOrderingKey>(CastSearchModel model, Ordering<Cast, TOrderingKey> ordering);
    }
}
