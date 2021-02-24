namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TagsBooks",
                c => new
                    {
                        Tags_Id = c.Int(nullable: false),
                        Books_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_Id, t.Books_Id })
                .ForeignKey("dbo.Tags", t => t.Tags_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Books_Id, cascadeDelete: true)
                .Index(t => t.Tags_Id)
                .Index(t => t.Books_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsBooks", "Books_Id", "dbo.Books");
            DropForeignKey("dbo.TagsBooks", "Tags_Id", "dbo.Tags");
            DropIndex("dbo.TagsBooks", new[] { "Books_Id" });
            DropIndex("dbo.TagsBooks", new[] { "Tags_Id" });
            DropTable("dbo.TagsBooks");
        }
    }
}
