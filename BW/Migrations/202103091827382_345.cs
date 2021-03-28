namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _345 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LoginTime", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "LastPing", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastPing", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LoginTime", c => c.DateTime(nullable: false));
        }
    }
}
