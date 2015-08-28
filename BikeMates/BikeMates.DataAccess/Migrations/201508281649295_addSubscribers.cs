namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubscribers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Route_Id", "dbo.Routes");
            DropIndex("dbo.Routes", new[] { "User_Id" });
            DropIndex("dbo.Routes", new[] { "User_Id1" });
            DropIndex("dbo.Users", new[] { "Route_Id" });
            DropColumn("dbo.Routes", "Author_Id");
            RenameColumn(table: "dbo.Routes", name: "User_Id1", newName: "Author_Id");
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
            
            DropColumn("dbo.Routes", "User_Id");
            DropColumn("dbo.Users", "Route_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Route_Id", c => c.Int());
            AddColumn("dbo.Routes", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Subscriptions", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Subscriptions", "User_Id", "dbo.Users");
            DropIndex("dbo.Subscriptions", new[] { "Route_Id" });
            DropIndex("dbo.Subscriptions", new[] { "User_Id" });
            DropTable("dbo.Subscriptions");
            RenameColumn(table: "dbo.Routes", name: "Author_Id", newName: "User_Id1");
            AddColumn("dbo.Routes", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "Route_Id");
            CreateIndex("dbo.Routes", "User_Id1");
            CreateIndex("dbo.Routes", "User_Id");
            AddForeignKey("dbo.Users", "Route_Id", "dbo.Routes", "Id");
            AddForeignKey("dbo.Routes", "User_Id", "dbo.Users", "Id");
        }
    }
}
