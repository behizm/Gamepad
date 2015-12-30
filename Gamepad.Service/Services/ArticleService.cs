using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;

namespace Gamepad.Service.Services
{
    internal class ArticleService : BaseService<Article>, IArticleService
    {
        public ArticleService(GamepadContext context) : base(context)
        {
        }

        public IArticleService Clone()
        {
            return new ArticleService(new GamepadContext());
        }
    }
}
