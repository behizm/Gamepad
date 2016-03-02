using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using Gamepad.Service.Data;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;
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

        public static OperationResult SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return OperationResult.Success;
            }
            catch (DbEntityValidationException exception)
            {
                return OperationResult.Failed(exception, ErrorMessages.Services_General_InputData);
            }
            catch (Exception exception)
            {
                return OperationResult.Failed(exception, ErrorMessages.Services_General_OperationError);
            }
        }

        public static async Task<OperationResult> SaveChangesAsync()
        {
            var operationResult = OperationResult.Success;
            var task = Task.Run(() =>
            {
                try
                {
                    Context.SaveChanges();
                }
                catch (DbEntityValidationException exception)
                {
                    operationResult = OperationResult.Failed(exception.Message, "خطا از سمت پایگاه داده");
                }
                catch (Exception exception)
                {
                    operationResult = OperationResult.Failed(exception.Message, "خطا از سمت پایگاه داده");
                }
            });
            await task;
            return operationResult;
        }


        public static IUserService User { get; } = new UserService(Context);
        public static IRoleService Role { get; } = new RoleService(Context);
        public static IPermissionService Permission { get; } = new PermissionService(Context);
        public static IFileService File { get; } = new FileService(Context);
        public static IGenreService Genre { get; } = new GenreService(Context);
        public static ICastService Cast { get; } = new CastService(Context);
        public static ISystemHardwareService SystemHardware { get; } = new SystemHardwareService(Context);
        public static IArticleService Article { get; } = new ArticleService(Context);
        public static IUserReviewService UserReview { get; } = new UserReviewService(Context);
        public static IConfigService Config { get; } = new ConfigService(Context);
        public static IExternalRankService ExternalRank { get; } = new ExternalRankService(Context);
        public static IRateService Rate { get; } = new RateService(Context);
        public static ISystemRequirmentService SystemRequirment { get; } = new SystemRequirmentService(Context);
        public static IPostService Post { get; } = new PostService(Context);
        public static IPostCommentService PostComment { get; } = new PostCommentService(Context);
        public static IPollService PollService{ get; } = new PollService(Context);
    }
}
