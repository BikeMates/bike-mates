namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Name", c => c.String());
            AddColumn("dbo.Routes", "Description", c => c.String());
            AddColumn("dbo.Routes", "MeetingPlace", c => c.String());
            AddColumn("dbo.Routes", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Routes", "ParticipantsCount", c => c.Int(nullable: false));
            AddColumn("dbo.Routes", "Distance", c => c.Int(nullable: false));
            AddColumn("dbo.Routes", "IsBanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "IsBanned");
            DropColumn("dbo.Routes", "Distance");
            DropColumn("dbo.Routes", "ParticipantsCount");
            DropColumn("dbo.Routes", "Start");
            DropColumn("dbo.Routes", "MeetingPlace");
            DropColumn("dbo.Routes", "Description");
            DropColumn("dbo.Routes", "Name");
        }
    }
}
