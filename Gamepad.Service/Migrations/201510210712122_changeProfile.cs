namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrustRates", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TrustRates", "UserId");
            AddForeignKey("dbo.TrustRates", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrustRates", "UserId", "dbo.Users");
            DropIndex("dbo.TrustRates", new[] { "UserId" });
            DropColumn("dbo.TrustRates", "UserId");
        }
    }
}
