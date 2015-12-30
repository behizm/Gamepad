﻿using Gamepad.Service.Data;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Services;

// ReSharper disable once CheckNamespace
namespace Gamepad
{
    public static class GpServices
    {
        static GpServices()
        {
            EventManager.Laod();
        }

        private static readonly GamepadContext Context = new GamepadContext();

        public static IUserService User { get; } = new UserService(Context);
        public static IRoleService Role { get; } = new RoleService(Context);
        public static IPermissionService Permission { get; } = new PermissionService(Context);
        public static IFileService File { get; } = new FileService(Context);
        public static IGenreService Genre { get; } = new GenreService(Context);
        public static ICastService Cast { get; } = new CastService(Context);
        public static ISystemHardwareService SystemHardware { get; } = new SystemHardwareService(Context);
        public static IArticleService Article { get; } = new ArticleService(Context);
    }
}
