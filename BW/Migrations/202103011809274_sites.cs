namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sites",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        url = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sites", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.sites", new[] { "User_Id" });
            DropTable("dbo.sites");
        }
    }
}
