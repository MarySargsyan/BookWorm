namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gyg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.sites", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.sites", "networkicon_id", "dbo.networkicons");
            DropIndex("dbo.sites", new[] { "networkicon_id" });
            DropIndex("dbo.sites", new[] { "User_Id" });
            AddColumn("dbo.sites", "networkicon_id1", c => c.Int());
            AddColumn("dbo.sites", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.sites", "networkicon_id", c => c.Int(nullable: false));
            AlterColumn("dbo.sites", "User_Id", c => c.String());
            CreateIndex("dbo.sites", "networkicon_id1");
            CreateIndex("dbo.sites", "User_Id1");
            AddForeignKey("dbo.sites", "User_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.sites", "networkicon_id1", "dbo.networkicons", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sites", "networkicon_id1", "dbo.networkicons");
            DropForeignKey("dbo.sites", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.sites", new[] { "User_Id1" });
            DropIndex("dbo.sites", new[] { "networkicon_id1" });
            AlterColumn("dbo.sites", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.sites", "networkicon_id", c => c.Int());
            DropColumn("dbo.sites", "User_Id1");
            DropColumn("dbo.sites", "networkicon_id1");
            CreateIndex("dbo.sites", "User_Id");
            CreateIndex("dbo.sites", "networkicon_id");
            AddForeignKey("dbo.sites", "networkicon_id", "dbo.networkicons", "id");
            AddForeignKey("dbo.sites", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
