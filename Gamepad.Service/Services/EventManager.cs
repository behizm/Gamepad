namespace Gamepad.Service.Services
{
    public class EventManager
    {
        public static void Laod()
        {
            //Services.User.UserAdded += Services.Genre.OnUserAdded;
            GpServices.UserReview.UserReviewAdded += GpServices.Article.OnUserReviewAdded;
        }
    }
}
