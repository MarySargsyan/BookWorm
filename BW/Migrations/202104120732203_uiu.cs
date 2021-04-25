namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uiu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chats", "Id", "dbo.Clubs");
            DropForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.ChatMessages", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Chats", new[] { "Id" });
            DropPrimaryKey("dbo.Chats");
            AddColumn("dbo.Clubs", "chat_Id", c => c.Int());
            AlterColumn("dbo.Chats", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Chats", "Id");
            CreateIndex("dbo.Clubs", "chat_Id");
            AddForeignKey("dbo.Clubs", "chat_Id", "dbo.Chats", "Id");
            AddForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatMessages", "Chat_Id", "dbo.Chats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatMessages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Clubs", "chat_Id", "dbo.Chats");
            DropIndex("dbo.Clubs", new[] { "chat_Id" });
            DropPrimaryKey("dbo.Chats");
            AlterColumn("dbo.Chats", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Clubs", "chat_Id");
            AddPrimaryKey("dbo.Chats", "Id");
            CreateIndex("dbo.Chats", "Id");
            AddForeignKey("dbo.ChatMessages", "Chat_Id", "dbo.Chats", "Id");
            AddForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Chats", "Id", "dbo.Clubs", "Id");
        }
    }
}
