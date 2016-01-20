using System;

namespace Gamepad.Service.Models.EventArgs
{
    public class UserReviewEventArgs : System.EventArgs
    {
        public Guid ArticleId { get; set; }
    }
}