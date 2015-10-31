using Gamepad.Service.Interfaces;
using Gamepad.Service.Repositories;

// ReSharper disable once CheckNamespace
namespace Gamepad
{
    public class Services
    {
        public static IUserService User { get; } = new UserService();
        public static IRoleService Role { get; } = new RoleService();
        public static IPermissionService Permission { get; } = new PermissionService();
        public static IFileService File { get; } = new FileService();
    }
}
