namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "Admin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "Admin");
        }
    }
}
