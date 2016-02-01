using System;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class PostCommentService : BaseService<PostComment>, IPostCommentService
    {
        public PostCommentService(GamepadContext context) : base(context)
        {
        }

        public IPostCommentService Clone()
        {
            return new PostCommentService(new GamepadContext());
        }

        public override OperationResult Insert(PostComment item)
        {
            return item.Post.IsForbiddenComment
                ? OperationResult.Failed(ErrorMessages.Services_General_NoAccess)
                : base.Insert(item);
        }

        public OperationResult ChangeConfirm(Guid commentId, bool isConfirmed)
        {
            var comment = FindById(commentId);
            if (comment == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            comment.IsConfirmed = isConfirmed;
            return Update(comment);
        }
    }
}