namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chats", "Friends_Id", "dbo.Friends");
            DropIndex("dbo.Chats", new[] { "Friends_Id" });
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chats", "Friends_Id", c => c.Int());
            CreateIndex("dbo.Chats", "Friends_Id");
        }
    }
}
