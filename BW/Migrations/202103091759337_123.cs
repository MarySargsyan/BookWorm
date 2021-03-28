namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChatMessages", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.ChatMessages", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChatMessages", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.ChatMessages", name: "UserId", newName: "User_Id");
        }
    }
}
