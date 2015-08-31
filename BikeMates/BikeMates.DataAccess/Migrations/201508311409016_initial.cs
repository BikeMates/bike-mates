namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        MapData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapDatas", t => t.MapData_Id)
                .Index(t => t.MapData_Id);
            
            CreateTable(
                "dbo.MapDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        End_Id = c.Int(),
                        Start_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordinates", t => t.End_Id)
                .ForeignKey("dbo.Coordinates", t => t.Start_Id)
                .Index(t => t.End_Id)
                .Index(t => t.Start_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        MeetingPlace = c.String(),
                        Start = c.DateTime(nullable: false),
                        Distance = c.Double(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        MapData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.MapDatas", t => t.MapData_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.MapData_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        Picture = c.String(),
                        About = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Route_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Route_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "MapData_Id", "dbo.MapDatas");
            DropForeignKey("dbo.Subscriptions", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Subscriptions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Routes", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Coordinates", "MapData_Id", "dbo.MapDatas");
            DropForeignKey("dbo.MapDatas", "Start_Id", "dbo.Coordinates");
            DropForeignKey("dbo.MapDatas", "End_Id", "dbo.Coordinates");
            DropIndex("dbo.Subscriptions", new[] { "Route_Id" });
            DropIndex("dbo.Subscriptions", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Routes", new[] { "MapData_Id" });
            DropIndex("dbo.Routes", new[] { "Author_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.MapDatas", new[] { "Start_Id" });
            DropIndex("dbo.MapDatas", new[] { "End_Id" });
            DropIndex("dbo.Coordinates", new[] { "MapData_Id" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Routes");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.MapDatas");
            DropTable("dbo.Coordinates");
        }
    }
}
