using System;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IPostCommentService : IBaseService<IPostCommentService, PostComment>
    {
        OperationResult ChangeConfirm(Guid commentId, bool isConfirmed);
    }
}