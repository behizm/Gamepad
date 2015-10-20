using Gamepad.Service.Interfaces;
using Gamepad.Service.Repositories;

// ReSharper disable once CheckNamespace
namespace Gamepad
{
    public class Services
    {
        public static IUserService User { get; } = new UserService();
        public static IFileService File { get; } = new FileService();
    }
}
