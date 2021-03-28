namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rabotay_umolyayu : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.sites", new[] { "networkicon_id1" });
            DropIndex("dbo.sites", new[] { "User_Id1" });
            DropColumn("dbo.sites", "User_Id");
            DropColumn("dbo.sites", "networkicon_id");
            RenameColumn(table: "dbo.sites", name: "User_Id1", newName: "User_Id");
            RenameColumn(table: "dbo.sites", name: "networkicon_id1", newName: "networkicon_id");
            AlterColumn("dbo.sites", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.sites", "networkicon_id", c => c.Int());
            CreateIndex("dbo.sites", "networkicon_id");
            CreateIndex("dbo.sites", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.sites", new[] { "User_Id" });
            DropIndex("dbo.sites", new[] { "networkicon_id" });
            AlterColumn("dbo.sites", "networkicon_id", c => c.Int(nullable: false));
            AlterColumn("dbo.sites", "User_Id", c => c.String());
            RenameColumn(table: "dbo.sites", name: "networkicon_id", newName: "networkicon_id1");
            RenameColumn(table: "dbo.sites", name: "User_Id", newName: "User_Id1");
            AddColumn("dbo.sites", "networkicon_id", c => c.Int(nullable: false));
            AddColumn("dbo.sites", "User_Id", c => c.String());
            CreateIndex("dbo.sites", "User_Id1");
            CreateIndex("dbo.sites", "networkicon_id1");
        }
    }
}
