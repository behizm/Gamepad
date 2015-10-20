namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserEnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsEmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "LastLoginDate", c => c.DateTime());
            DropColumn("dbo.Users", "Mobile");
            DropColumn("dbo.Users", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Mobile", c => c.String(maxLength: 15));
            DropColumn("dbo.Users", "LastLoginDate");
            DropColumn("dbo.Users", "IsEmailConfirmed");
        }
    }
}
