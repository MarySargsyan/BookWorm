namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "Readingbook", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "Readingbook");
        }
    }
}
