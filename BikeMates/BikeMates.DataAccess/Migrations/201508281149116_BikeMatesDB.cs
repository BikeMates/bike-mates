namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BikeMatesDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "IdentityRoles");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "IdentityUserLogins");
            DropForeignKey("dbo.Routes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.IdentityRoles", "RoleNameIndex");
            DropIndex("dbo.IdentityUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Routes", new[] { "User_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "Route_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "UserId" });
            DropPrimaryKey("dbo.IdentityUserRoles");
            DropPrimaryKey("dbo.IdentityUserLogins");
            CreateTable(
                "dbo.SubscriptionsManyToMany",
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
            
            AddColumn("dbo.IdentityUserRoles", "IdentityRole_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.IdentityUserLogins", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.IdentityRoles", "Name", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.IdentityUserLogins", "LoginProvider", c => c.String());
            AlterColumn("dbo.IdentityUserLogins", "ProviderKey", c => c.String());
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "RoleId", "UserId" });
            AddPrimaryKey("dbo.IdentityUserLogins", "UserId");
            CreateIndex("dbo.IdentityUserRoles", "IdentityRole_Id");
            CreateIndex("dbo.IdentityUserClaims", "UserId");
            CreateIndex("dbo.IdentityUserLogins", "User_Id");
            AddForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles", "Id");
            AddForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Routes", "User_Id");
            DropColumn("dbo.Users", "Route_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Route_Id", c => c.Int());
            AddColumn("dbo.Routes", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.SubscriptionsManyToMany", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.SubscriptionsManyToMany", "User_Id", "dbo.Users");
            DropIndex("dbo.SubscriptionsManyToMany", new[] { "Route_Id" });
            DropIndex("dbo.SubscriptionsManyToMany", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropPrimaryKey("dbo.IdentityUserLogins");
            DropPrimaryKey("dbo.IdentityUserRoles");
            AlterColumn("dbo.IdentityUserLogins", "ProviderKey", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.IdentityUserLogins", "LoginProvider", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.IdentityRoles", "Name", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.IdentityUserLogins", "User_Id");
            DropColumn("dbo.IdentityUserRoles", "IdentityRole_Id");
            DropTable("dbo.SubscriptionsManyToMany");
            AddPrimaryKey("dbo.IdentityUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "UserId", "RoleId" });
            CreateIndex("dbo.IdentityUserLogins", "UserId");
            CreateIndex("dbo.IdentityUserClaims", "UserId");
            CreateIndex("dbo.Users", "Route_Id");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Routes", "User_Id");
            CreateIndex("dbo.IdentityUserRoles", "RoleId");
            CreateIndex("dbo.IdentityRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Route_Id", "dbo.Routes", "Id");
            AddForeignKey("dbo.Routes", "User_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.IdentityRoles", newName: "AspNetRoles");
        }
    }
}
