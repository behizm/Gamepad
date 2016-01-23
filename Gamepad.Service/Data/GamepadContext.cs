using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Liberary;
using Gamepad.Service.Utilities.Async;

namespace Gamepad.Service.Data
{
    internal class GamepadContext : DbContext
    {
        public GamepadContext()
            : base("name=GamepadContext")
        {
            Database.SetInitializer(new GamepadContextInitializer());
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleInfo> ArticleInfos { get; set; }
        public DbSet<ArticlePlatform> ArticlePlatforms { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<ExternalRank> ExternalRanks { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollChoice> PollChoices { get; set; }
        public DbSet<PollUserAnswer> PollUserAnswers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostContent> PostContents { get; set; }
        public DbSet<PostReview> PostReviews { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RateContent> RateContents { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SystemRequirment> SystemRequirments { get; set; }
        public DbSet<TrackingArticle> TrackingArticles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<UserReviewLike> UserReviewLikes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema("Blog");
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Post>().HasMany<File>(s => s.Branches).WithMany(c => c.Users)
            //    .Map(c => c.MapLeftKey("User_id").MapRightKey("Branch_id").ToTable("UserBranches", "App"));
        }

        public void Seed(GamepadContext context)
        {
            var admin = context.Users.FirstOrDefault(x => x.Username == "admin");
            if (admin != null)
                return;

            var role = context.Roles.FirstOrDefault(x => x.Name == "superadmin");

            var adminUser = new User
            {
                AccessFailed = 0,
                Email = "behnam.zeighami@gmail.com",
                IsEmailConfirmed = true,
                IsLock = false,
                PasswordHash = AsyncTools.ConvertToSync(() => GamepadHashSystem.EncryptAsync("admin")),
                Username = "admin",
                Roles = new List<Role>
                {
                    role ?? new Role {Name = "superadmin"}
                }
            };
            context.Users.Add(adminUser);
        }
    }
}
