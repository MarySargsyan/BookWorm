namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BD", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BD");
        }
    }
}
