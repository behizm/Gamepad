using System;

namespace Gamepad.Service.Models.EventArgs
{
    public class UserEventArgs : System.EventArgs
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
