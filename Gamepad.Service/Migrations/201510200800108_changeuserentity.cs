namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuserentity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "AvatarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AvatarId", c => c.Guid());
        }
    }
}
