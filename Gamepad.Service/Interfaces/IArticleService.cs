using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Services;

namespace Gamepad.Service.Interfaces
{
    public interface IArticleService : IBaseService<IArticleService, Article>
    {
        Article FindByTitle(string title);
        Article FindByName(string name);
        string ChangePoster(Guid articleId, Guid posterId);
    }
}
