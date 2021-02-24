namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "LoginTime");
            DropColumn("dbo.AspNetUsers", "LastPing");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastPing", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LoginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
        }
    }
}
