using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Liberary;
using Gamepad.Utility.Async;

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
        public DbSet<Cast> Casts { get; set; }
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
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Post>().HasMany<File>(s => s.Branches).WithMany(c => c.Users)
            //    .Map(c => c.MapLeftKey("User_id").MapRightKey("Branch_id").ToTable("UserBranches", "App"));
        }

        public void Seed(GamepadContext context)
        {
            var adminUser = new User
            {
                AccessFailed = 0,
                Email = "behnam.zeighami@gmail.com",
                IsActive = true,
                IsLock = false,
                PasswordHash = AsyncTools.ConvertToSync(() => SquirrelHashSystem.EncryptAsync("admin")),
                Username = "admin",
                Roles = new List<Role>
                {
                    new Role {Name = "superadmin"}
                }
            };
            context.Users.Add(adminUser);
        }
    }
}
