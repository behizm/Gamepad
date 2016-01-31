namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePostEnt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "File_Id", "dbo.Files");
            DropForeignKey("dbo.Files", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Articles", new[] { "File_Id" });
            DropIndex("dbo.Files", new[] { "Article_Id" });
            CreateTable(
                "dbo.ArticleImageGallery",
                c => new
                    {
                        ArticleId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.FileId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.FileId);
            
            DropColumn("dbo.Articles", "File_Id");
            DropColumn("dbo.Files", "Article_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Article_Id", c => c.Guid());
            AddColumn("dbo.Articles", "File_Id", c => c.Guid());
            DropForeignKey("dbo.ArticleImageGallery", "FileId", "dbo.Files");
            DropForeignKey("dbo.ArticleImageGallery", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticleImageGallery", new[] { "FileId" });
            DropIndex("dbo.ArticleImageGallery", new[] { "ArticleId" });
            DropTable("dbo.ArticleImageGallery");
            CreateIndex("dbo.Files", "Article_Id");
            CreateIndex("dbo.Articles", "File_Id");
            AddForeignKey("dbo.Files", "Article_Id", "dbo.Articles", "Id");
            AddForeignKey("dbo.Articles", "File_Id", "dbo.Files", "Id");
        }
    }
}
