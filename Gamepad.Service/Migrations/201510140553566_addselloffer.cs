namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addselloffer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrustRates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Rate = c.Short(nullable: false),
                        Comment = c.String(maxLength: 500),
                        ProfileId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            AddColumn("dbo.Profiles", "Alias", c => c.String(maxLength: 50));
            AddColumn("dbo.Profiles", "TrustRateAverage", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrustRates", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.TrustRates", new[] { "ProfileId" });
            DropColumn("dbo.Profiles", "TrustRateAverage");
            DropColumn("dbo.Profiles", "Alias");
            DropTable("dbo.TrustRates");
        }
    }
}
