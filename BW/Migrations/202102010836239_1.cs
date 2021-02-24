namespace BW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Discription = c.String(),
                        Raiting = c.Double(nullable: false),
                        Link = c.String(),
                        Tags_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.Tags_Id)
                .Index(t => t.Tags_Id);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Discription = c.String(),
                        Icon = c.String(),
                        Image = c.String(),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BirthDate = c.DateTime(),
                        City = c.String(),
                        about = c.String(),
                        Image = c.String(),
                        LoginTime = c.DateTime(nullable: false),
                        LastPing = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Chat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Chat_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ClubsBooks",
                c => new
                    {
                        Clubs_Id = c.Int(nullable: false),
                        Books_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clubs_Id, t.Books_Id })
                .ForeignKey("dbo.Clubs", t => t.Clubs_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Books_Id, cascadeDelete: true)
                .Index(t => t.Clubs_Id)
                .Index(t => t.Books_Id);
            
            CreateTable(
                "dbo.TagsClubs",
                c => new
                    {
                        Tags_Id = c.Int(nullable: false),
                        Clubs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_Id, t.Clubs_Id })
                .ForeignKey("dbo.Tags", t => t.Tags_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clubs", t => t.Clubs_Id, cascadeDelete: true)
                .Index(t => t.Tags_Id)
                .Index(t => t.Clubs_Id);
            
            CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Books_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Books_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Books_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Books_Id);
            
            CreateTable(
                "dbo.ApplicationUserClubs",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Clubs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Clubs_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clubs", t => t.Clubs_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Clubs_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ChatMessages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.ChatMessages", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserClubs", "Clubs_Id", "dbo.Clubs");
            DropForeignKey("dbo.ApplicationUserClubs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserBooks", "Books_Id", "dbo.Books");
            DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagsClubs", "Clubs_Id", "dbo.Clubs");
            DropForeignKey("dbo.TagsClubs", "Tags_Id", "dbo.Tags");
            DropForeignKey("dbo.Books", "Tags_Id", "dbo.Tags");
            DropForeignKey("dbo.ClubsBooks", "Books_Id", "dbo.Books");
            DropForeignKey("dbo.ClubsBooks", "Clubs_Id", "dbo.Clubs");
            DropIndex("dbo.ApplicationUserClubs", new[] { "Clubs_Id" });
            DropIndex("dbo.ApplicationUserClubs", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "Books_Id" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TagsClubs", new[] { "Clubs_Id" });
            DropIndex("dbo.TagsClubs", new[] { "Tags_Id" });
            DropIndex("dbo.ClubsBooks", new[] { "Books_Id" });
            DropIndex("dbo.ClubsBooks", new[] { "Clubs_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ChatMessages", new[] { "Chat_Id" });
            DropIndex("dbo.ChatMessages", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Books", new[] { "Tags_Id" });
            DropTable("dbo.ApplicationUserClubs");
            DropTable("dbo.ApplicationUserBooks");
            DropTable("dbo.TagsClubs");
            DropTable("dbo.ClubsBooks");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.Chats");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.Clubs");
            DropTable("dbo.Books");
        }
    }
}
