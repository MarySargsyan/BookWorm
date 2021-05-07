namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pliznu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.networkicons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        ico = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.sites", "networkicon_id", c => c.Int());
            CreateIndex("dbo.sites", "networkicon_id");
            AddForeignKey("dbo.sites", "networkicon_id", "dbo.networkicons", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sites", "networkicon_id", "dbo.networkicons");
            DropIndex("dbo.sites", new[] { "networkicon_id" });
            DropColumn("dbo.sites", "networkicon_id");
            DropTable("dbo.networkicons");
        }
    }
}
