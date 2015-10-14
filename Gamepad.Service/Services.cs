using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Repositories;

namespace Gamepad
{
    public class Services
    {
        public static IUserService User { get; } = new UserService();
    }
}
