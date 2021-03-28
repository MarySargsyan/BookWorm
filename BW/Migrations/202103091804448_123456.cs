namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123456 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChatMessages", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.ChatMessages", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChatMessages", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.ChatMessages", name: "User_Id", newName: "UserId");
        }
    }
}
