using System;
using System.Linq.Expressions;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.EventArgs;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Models.ViewModels;
using Gamepad.Service.Resources;
using Gamepad.Service.Utilities.Models;

namespace Gamepad.Service.Services
{
    internal class GenreService : BaseService<Genre>, IGenreService
    {
        public GenreService(GamepadContext context) : base(context)
        {
        }

        public IGenreService Clone()
        {
            return new GenreService(new GamepadContext());
        }

        public override OperationResult Insert(Genre item)
        {
            var genre =
                Get(x =>
                    x.Name.ToLower() == item.Name.ToLower() ||
                    x.FaName.ToLower() == item.FaName.ToLower());

            if (genre != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Insert(item);
        }

        public Genre FindByName(string name)
        {
            name = name.ToLower();
            return Get(x => x.Name.ToLower() == name || x.FaName.ToLower() == name);
        }

        public override OperationResult Update(Genre item)
        {
            var genre =
                Get(x =>
                    x.Id != item.Id &&
                    (x.Name.ToLower() == item.Name.ToLower() ||
                     x.FaName.ToLower() == item.FaName.ToLower()));

            if (genre != null)
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);

            return base.Update(item);
        }

        public Cluster<Genre> Search<TOrderingKey>(GenreSearchModel model, Ordering<Genre, TOrderingKey> ordering)
        {
            Expression<Func<Genre, bool>> expression;
            if (model == null)
            {
                expression = u => true;
            }
            else
            {
                expression = u =>
                    (string.IsNullOrEmpty(model.Name) || u.Name.ToLower().Contains(model.Name)) &&
                    (string.IsNullOrEmpty(model.FaName) || u.FaName.ToLower().Contains(model.FaName));
            }
            return Search(expression, ordering);
        }

        // Events Listeners
        public void OnUserAdded(object sender, UserEventArgs user)
        {
            Insert(new Genre
            {
                Name = "for add user " + user.Username,
                FaName = "افزوده شدن کاربر",
            });
        }
    }
}
