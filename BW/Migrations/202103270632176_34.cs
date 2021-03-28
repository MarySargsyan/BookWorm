namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendsApplicationUsers",
                c => new
                    {
                        Friends_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Friends_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Friends", t => t.Friends_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Friends_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendsApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendsApplicationUsers", "Friends_Id", "dbo.Friends");
            DropIndex("dbo.FriendsApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendsApplicationUsers", new[] { "Friends_Id" });
            DropTable("dbo.FriendsApplicationUsers");
            DropTable("dbo.Friends");
        }
    }
}
