namespace Gamepad.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefileentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Category");
        }
    }
}
