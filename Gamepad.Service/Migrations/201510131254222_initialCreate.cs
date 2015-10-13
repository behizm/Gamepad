namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleInfoes",
                c => new
                    {
                        ArticleId = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        Website = c.String(),
                        FinishTimeMain = c.Short(),
                        FinishTimeAverage = c.Short(),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.Int(nullable: false),
                        EditDate = c.DateTime(),
                        ReleaseDate = c.DateTime(),
                        Platform = c.Int(),
                        SiteScore = c.Short(),
                        UserScoresAverage = c.Short(),
                        PosterId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.PosterId)
                .Index(t => t.PosterId);
            
            CreateTable(
                "dbo.Communities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Body = c.String(nullable: false, maxLength: 500),
                        ArticleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(maxLength: 15),
                        PasswordHash = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AccessFailed = c.Short(nullable: false),
                        IsLock = c.Boolean(nullable: false),
                        LockedDate = c.DateTime(),
                        AvatarId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAvatars",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Files", t => t.FileId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Filename = c.String(nullable: false, maxLength: 50),
                        Size = c.Int(nullable: false),
                        FileType = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        EditDate = c.DateTime(),
                        CreatorId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Article_Id = c.Guid(),
                        Post_Id = c.Guid(),
                        Post_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id1)
                .Index(t => t.CreatorId)
                .Index(t => t.Article_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.Post_Id1);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Firstname = c.String(maxLength: 50),
                        Lastname = c.String(maxLength: 50),
                        Company = c.String(maxLength: 100),
                        Website = c.String(maxLength: 300),
                        ProfileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Area = c.String(nullable: false, maxLength: 30),
                        Controller = c.String(nullable: false, maxLength: 30),
                        Action = c.String(nullable: false, maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrackingArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Casts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CastType = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 50),
                        FaValue = c.String(nullable: false, maxLength: 50),
                        ArticleId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ExternalRanks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Score = c.Short(nullable: false),
                        Url = c.String(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        FaName = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 200),
                        PostType = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        EditDate = c.DateTime(),
                        PublishDate = c.DateTime(),
                        View = c.Long(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        MainImageId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        Video_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Files", t => t.MainImageId)
                .ForeignKey("dbo.Files", t => t.Video_Id)
                .Index(t => t.AuthorId)
                .Index(t => t.MainImageId)
                .Index(t => t.Video_Id);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false, maxLength: 300),
                        IsForbiddenComment = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                        PostId = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostComments", t => t.ParentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.PostContents",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        Post_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.PostReviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Max = c.Short(nullable: false),
                        Score = c.Short(nullable: false),
                        PostId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RateSource = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.RateContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RateSource = c.Int(nullable: false),
                        Content = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Rate_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rates", t => t.Rate_Id)
                .Index(t => t.Rate_Id);
            
            CreateTable(
                "dbo.SystemRequirments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsMinimum = c.Boolean(nullable: false),
                        SystemPart = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        Article_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 500),
                        Score = c.Short(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        DislikeCount = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.PollChoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        IsCurrect = c.Boolean(nullable: false),
                        PollId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollId)
                .Index(t => t.PollId);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Body = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false, maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        MaxChoiceCount = c.Short(nullable: false),
                        ExpiredDate = c.DateTime(),
                        ArticleId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.PollUserAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParticipantIp = c.String(nullable: false, maxLength: 15),
                        PollId = c.Guid(nullable: false),
                        PollChoiceId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollId)
                .ForeignKey("dbo.PollChoices", t => t.PollChoiceId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.PollId)
                .Index(t => t.PollChoiceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserReviewLikes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Like = c.Boolean(nullable: false),
                        UserReviewId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.UserReviews", t => t.UserReviewId)
                .Index(t => t.UserReviewId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PermissionRoles",
                c => new
                    {
                        Permission_Id = c.Guid(nullable: false),
                        Role_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Role_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.GenreArticles",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Article_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Article_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.PostArticles",
                c => new
                    {
                        Post_Id = c.Guid(nullable: false),
                        Article_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_Id, t.Article_Id })
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Post_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserReviewLikes", "UserReviewId", "dbo.UserReviews");
            DropForeignKey("dbo.UserReviewLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.PollChoices", "PollId", "dbo.Polls");
            DropForeignKey("dbo.PollUserAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.PollUserAnswers", "PollChoiceId", "dbo.PollChoices");
            DropForeignKey("dbo.PollUserAnswers", "PollId", "dbo.Polls");
            DropForeignKey("dbo.Polls", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleInfoes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.UserReviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserReviews", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.SystemRequirments", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.RateContents", "Rate_Id", "dbo.Rates");
            DropForeignKey("dbo.Rates", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Posts", "Video_Id", "dbo.Files");
            DropForeignKey("dbo.PostReviews", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostContents", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.PostComments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostComments", "ParentId", "dbo.PostComments");
            DropForeignKey("dbo.Posts", "MainImageId", "dbo.Files");
            DropForeignKey("dbo.Files", "Post_Id1", "dbo.Posts");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Files", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.PostArticles", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Articles", "PosterId", "dbo.Files");
            DropForeignKey("dbo.Files", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.GenreArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.GenreArticles", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.ExternalRanks", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Casts", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Communities", "UserId", "dbo.Users");
            DropForeignKey("dbo.TrackingArticles", "UserId", "dbo.Users");
            DropForeignKey("dbo.TrackingArticles", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.PermissionRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.PermissionRoles", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAvatars", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAvatars", "FileId", "dbo.Files");
            DropForeignKey("dbo.Files", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Communities", "ArticleId", "dbo.Articles");
            DropIndex("dbo.PostArticles", new[] { "Article_Id" });
            DropIndex("dbo.PostArticles", new[] { "Post_Id" });
            DropIndex("dbo.GenreArticles", new[] { "Article_Id" });
            DropIndex("dbo.GenreArticles", new[] { "Genre_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.PermissionRoles", new[] { "Role_Id" });
            DropIndex("dbo.PermissionRoles", new[] { "Permission_Id" });
            DropIndex("dbo.UserReviewLikes", new[] { "UserId" });
            DropIndex("dbo.UserReviewLikes", new[] { "UserReviewId" });
            DropIndex("dbo.PollUserAnswers", new[] { "UserId" });
            DropIndex("dbo.PollUserAnswers", new[] { "PollChoiceId" });
            DropIndex("dbo.PollUserAnswers", new[] { "PollId" });
            DropIndex("dbo.Polls", new[] { "ArticleId" });
            DropIndex("dbo.PollChoices", new[] { "PollId" });
            DropIndex("dbo.UserReviews", new[] { "ArticleId" });
            DropIndex("dbo.UserReviews", new[] { "UserId" });
            DropIndex("dbo.SystemRequirments", new[] { "Article_Id" });
            DropIndex("dbo.RateContents", new[] { "Rate_Id" });
            DropIndex("dbo.Rates", new[] { "ArticleId" });
            DropIndex("dbo.PostReviews", new[] { "PostId" });
            DropIndex("dbo.PostTags", new[] { "Post_Id" });
            DropIndex("dbo.PostContents", new[] { "PostId" });
            DropIndex("dbo.PostComments", new[] { "ParentId" });
            DropIndex("dbo.PostComments", new[] { "PostId" });
            DropIndex("dbo.PostComments", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "Video_Id" });
            DropIndex("dbo.Posts", new[] { "MainImageId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.ExternalRanks", new[] { "ArticleId" });
            DropIndex("dbo.Casts", new[] { "ArticleId" });
            DropIndex("dbo.TrackingArticles", new[] { "UserId" });
            DropIndex("dbo.TrackingArticles", new[] { "ArticleId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Files", new[] { "Post_Id1" });
            DropIndex("dbo.Files", new[] { "Post_Id" });
            DropIndex("dbo.Files", new[] { "Article_Id" });
            DropIndex("dbo.Files", new[] { "CreatorId" });
            DropIndex("dbo.UserAvatars", new[] { "FileId" });
            DropIndex("dbo.UserAvatars", new[] { "UserId" });
            DropIndex("dbo.Communities", new[] { "UserId" });
            DropIndex("dbo.Communities", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "PosterId" });
            DropIndex("dbo.ArticleInfoes", new[] { "ArticleId" });
            DropTable("dbo.PostArticles");
            DropTable("dbo.GenreArticles");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.PermissionRoles");
            DropTable("dbo.UserReviewLikes");
            DropTable("dbo.PollUserAnswers");
            DropTable("dbo.Polls");
            DropTable("dbo.PollChoices");
            DropTable("dbo.UserReviews");
            DropTable("dbo.SystemRequirments");
            DropTable("dbo.RateContents");
            DropTable("dbo.Rates");
            DropTable("dbo.PostReviews");
            DropTable("dbo.PostTags");
            DropTable("dbo.PostContents");
            DropTable("dbo.PostComments");
            DropTable("dbo.Posts");
            DropTable("dbo.Genres");
            DropTable("dbo.ExternalRanks");
            DropTable("dbo.Casts");
            DropTable("dbo.TrackingArticles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Profiles");
            DropTable("dbo.Files");
            DropTable("dbo.UserAvatars");
            DropTable("dbo.Users");
            DropTable("dbo.Communities");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleInfoes");
        }
    }
}
