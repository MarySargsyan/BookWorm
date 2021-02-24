namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Tags_Id", "dbo.Tags");
            DropIndex("dbo.Books", new[] { "Tags_Id" });
            DropColumn("dbo.Books", "Tags_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Tags_Id", c => c.Int());
            CreateIndex("dbo.Books", "Tags_Id");
            AddForeignKey("dbo.Books", "Tags_Id", "dbo.Tags", "Id");
        }
    }
}
