namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserClubs", newName: "ClubsApplicationUsers");
            DropPrimaryKey("dbo.ClubsApplicationUsers");
            AddPrimaryKey("dbo.ClubsApplicationUsers", new[] { "Clubs_Id", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ClubsApplicationUsers");
            AddPrimaryKey("dbo.ClubsApplicationUsers", new[] { "ApplicationUser_Id", "Clubs_Id" });
            RenameTable(name: "dbo.ClubsApplicationUsers", newName: "ApplicationUserClubs");
        }
    }
}
