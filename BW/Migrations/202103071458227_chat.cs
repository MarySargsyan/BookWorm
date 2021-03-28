namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatApplicationUsers",
                c => new
                    {
                        Chat_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Chat_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Chats", t => t.Chat_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Chat_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "LoginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastPing", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.ChatApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ChatApplicationUsers", new[] { "Chat_Id" });
            DropColumn("dbo.AspNetUsers", "LastPing");
            DropColumn("dbo.AspNetUsers", "LoginTime");
            DropTable("dbo.ChatApplicationUsers");
        }
    }
}
