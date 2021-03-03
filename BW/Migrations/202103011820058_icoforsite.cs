namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class icoforsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sites", "ico", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.sites", "ico");
        }
    }
}
