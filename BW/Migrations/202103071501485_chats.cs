namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatMessages", "Date", c => c.DateTime());
            AddColumn("dbo.ChatMessages", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatMessages", "Text");
            DropColumn("dbo.ChatMessages", "Date");
        }
    }
}
