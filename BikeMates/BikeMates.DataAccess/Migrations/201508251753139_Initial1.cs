namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Routes", new[] { "User_Id" });
            CreateTable(
                "dbo.UserRoutes",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Route_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Route_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Route_Id);
            
            DropColumn("dbo.Routes", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserRoutes", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.UserRoutes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserRoutes", new[] { "Route_Id" });
            DropIndex("dbo.UserRoutes", new[] { "User_Id" });
            DropTable("dbo.UserRoutes");
            CreateIndex("dbo.Routes", "User_Id");
            AddForeignKey("dbo.Routes", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
