using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IPostService : IBaseService<IPostService, Post>
    {
        Post FindByTitle(string title);
        Post FindByName(string name);
        OperationResult Save(Post item);
        OperationResult Publish(Guid postId, DateTime? publishDate = null);
        OperationResult ChangeHide(Guid postId, bool isHidden);
    }
}