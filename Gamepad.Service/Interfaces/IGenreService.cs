using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Interfaces
{
    public interface IGenreService : IBaseService<IGenreService, Genre>
    {
        Genre FindByName(string name);
        Cluster<Genre> Search<TOrderingKey>(GenreSearchModel model, Ordering<Genre, TOrderingKey> ordering);

        // Events Listeners
        void OnUserAdded(object sender, UserEventArgs user);
    }
}
