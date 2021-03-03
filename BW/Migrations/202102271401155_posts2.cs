namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Date", c => c.DateTime());
            AddColumn("dbo.Posts", "Text", c => c.String());
            DropColumn("dbo.Posts", "ApplicationUserId");
            DropColumn("dbo.Posts", "ClubId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ClubId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ApplicationUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "Text");
            DropColumn("dbo.Posts", "Date");
        }
    }
}
