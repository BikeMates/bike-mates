namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration //TODO: Rename this migration. Use more specific name in order to describe the changes for the database
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Routes");
            AlterColumn("dbo.Routes", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Routes", "Name");
            DropColumn("dbo.Routes", "Id");
            DropColumn("dbo.Routes", "IsBanned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Routes", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Routes");
            AlterColumn("dbo.Routes", "Name", c => c.String());
            AddPrimaryKey("dbo.Routes", "Id");
        }
    }
}
