namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ch : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ChatMessages", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatMessages", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
